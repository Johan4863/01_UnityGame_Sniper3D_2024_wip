using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the Line Renderer component
    public float maxDistance = 1000f; // Maximum distance the laser can travel
    public float laserOffset = 0.5f; // Offset for the starting position of the laser line
    public float ballSize = 0.1f; // Size of the ball
    public Color ballColor = Color.red; // Color of the ball
    public float forceMagnitude = 1f; // Force magnitude applied to the enemy

    private Camera playerCamera; // Reference to the player's camera
    private GameObject ballObject; // Reference to the instantiated ball object
    private GameObject lastHitEnemy; // Reference to the last enemy hit by the laser
    private Rigidbody lastHitEnemyRigidbody; // Reference to the Rigidbody of the last hit enemy

    void Start()
    {
        // Assign the laser to a specific rendering layer
        lineRenderer.gameObject.layer = LayerMask.NameToLayer("LaserLayer");
    }

    void Update()
    {
        // Find the player's camera dynamically
        FindPlayerCamera();

        // Cast a ray from the player's camera towards the mouse cursor position
        if (playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            // Set the start position of the laser slightly below the position of the player's camera
            Vector3 laserStartPosition = playerCamera.transform.position - playerCamera.transform.up * laserOffset;
            lineRenderer.SetPosition(0, laserStartPosition);

            RaycastHit hit;

            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Set the end position of the laser to the hit point
                lineRenderer.SetPosition(1, hit.point);

                // Check if the hit object is an enemy (tagged as "Soldier" or "Target")
                if (hit.collider.CompareTag("Soldier") || hit.collider.CompareTag("Target"))
                {
                    lastHitEnemy = hit.collider.gameObject;
                    lastHitEnemyRigidbody = hit.collider.attachedRigidbody;

                    // Spawn a ball around the enemy if it's not already spawned
                    if (ballObject == null)
                    {
                        SpawnBall(lastHitEnemy.transform.position);
                    }
                }
                else
                {
                    // Destroy the ball if the hit object is not an enemy
                    DestroyBall();
                }
            }
            else
            {
                // If the ray doesn't hit anything, extend the laser to its maximum distance
                Vector3 endPoint = ray.origin + ray.direction * maxDistance;
                lineRenderer.SetPosition(1, endPoint);

                // Destroy the ball if the ray doesn't hit anything
                DestroyBall();
            }

            // Disable rendering layer for all cameras except for the player's camera
            DisableRenderingLayerForAllCamerasExceptPlayer();

            // Check for shooting when the left mouse button is clicked
            Shoot();
        }
    }

    void FindPlayerCamera()
    {
        // Find the active camera in the scene
        Camera[] allCameras = Camera.allCameras;

        foreach (Camera camera in allCameras)
        {
            if (camera.isActiveAndEnabled)
            {
                playerCamera = camera;
                return;
            }
        }

        // If no active camera is found, set playerCamera to null
        playerCamera = null;
    }

    void SpawnBall(Vector3 position)
    {
        // Create a new GameObject for the ball
        ballObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Set the position of the ball around the enemy
        ballObject.transform.position = position;

        // Scale the ball to the desired size
        ballObject.transform.localScale = Vector3.one * ballSize;

        // Set the material color of the ball
        Renderer ballRenderer = ballObject.GetComponent<Renderer>();
        ballRenderer.material.color = ballColor;

        // Ensure the ball is not affected by physics
        Destroy(ballObject.GetComponent<Rigidbody>());
    }

    void DestroyBall()
    {
        // Destroy the ball object if it exists
        if (ballObject != null)
        {
            Destroy(ballObject);
            ballObject = null;
        }
    }
    void Shoot()
    {
        // Check for shooting when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && ballObject != null)
        {
            // Apply damage to the enemy
            EnemyController enemyController = lastHitEnemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(1); // Apply 1 damage to the enemy
            }
        }
    }

    void DisableRenderingLayerForAllCamerasExceptPlayer()
    {
        Camera[] allCameras = Camera.allCameras;

        foreach (Camera camera in allCameras)
        {
            if (camera != playerCamera)
            {
                // Disable the rendering layer for this camera
                camera.cullingMask &= ~(1 << LayerMask.NameToLayer("LaserLayer"));
            }
        }
    }
}
