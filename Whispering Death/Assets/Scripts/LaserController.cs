using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the Line Renderer component
    public float maxDistance = 1000f; // Maximum distance the laser can travel
    public float laserOffset = 0.5f; // Offset for the starting position of the laser line
    public float ballSize = 0.1f; // Size of the ball
    public float ballHeadSize = 0.1f; // Size of the head ball
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
        FindPlayerCamera();

        if (playerCamera != null)
        {
            CastLaserRay();
            DisableRenderingLayerForAllCamerasExceptPlayer();
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

    void CastLaserRay()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 laserStartPosition = playerCamera.transform.position - playerCamera.transform.up * laserOffset;
        lineRenderer.SetPosition(0, laserStartPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Soldier") || hit.collider.CompareTag("Target") || hit.collider.CompareTag("Civilian"))
            {
                SpawnBall(hit.collider.transform.position, ballSize);
            }
            else if (hit.collider.CompareTag("SoldierHead") || hit.collider.CompareTag("CivilianHead") || hit.collider.CompareTag("TargetHead"))
            {
                SpawnBall(hit.collider.transform.position, ballHeadSize);
            }
            else
            {
                DestroyBall();
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * maxDistance;
            lineRenderer.SetPosition(1, endPoint);
            DestroyBall();
        }

        if (Input.GetMouseButtonDown(0) && ballObject != null)
        {
            Shoot(hit);
        }
    }

    void SpawnBall(Vector3 position, float size)
    {
        DestroyBall();
        ballObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ballObject.transform.position = position;
        ballObject.transform.localScale = Vector3.one * size;
        Renderer ballRenderer = ballObject.GetComponent<Renderer>();
        ballRenderer.material.color = ballColor;
        Destroy(ballObject.GetComponent<Rigidbody>());
    }

    void DestroyBall()
    {
        if (ballObject != null)
        {
            Destroy(ballObject);
            ballObject = null;
        }
    }

    void Shoot(RaycastHit hit)
    {
        EnemyController enemyController = hit.collider.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            if (hit.collider.CompareTag("SoldierHead") || hit.collider.CompareTag("CivilianHead") || hit.collider.CompareTag("TargetHead"))
            {
                enemyController.TakeDamage(2); // Apply double damage to the enemy if hit in the head
            }
            else 
            {
                enemyController.TakeDamage(1); // Apply normal damage to the enemy
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
                camera.cullingMask &= ~(1 << LayerMask.NameToLayer("LaserLayer"));
            }
        }
    }
}
