using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour
{
    // AI movement related variables
    public Transform[] waypoints; // Array of waypoints AI will move towards
    public Transform[] alertedRoute; // Array of waypoints AI will move towards when alerted

    // Speed variables
    public float speed = 5f; // Speed of AI movement
    public float alertedSpeed = 10f; // Speed of AI movement when alerted

    // Delay variables
    public float delayBetweenWaypoints = 1f; // Delay between reaching each waypoint
    public float delayBetweenAlertedWaypoints = 0.6f; // Delay between waypoints when alerted

    // Rotation variables
    public float rotationIntensity = 30f; // Intensity of wobbly movement
    public float rotationSpeed = 5f; // Speed of rotation

    // Current waypoint index variables
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private int currentEscapeRouteIndex = 0; // Index of the current waypoint when alerted

    // Time tracking variables
    private float timeSinceReachedWaypoint = 0f; // Time elapsed since reaching the current waypoint
    private float alertedWaipontTime = 0f; // Time elapsed since reaching the current waypoint when alerted

    // Other variables
    private Vector3 originalPosition; // Original position of the AI
    private bool isRed = false; // Flag to track if objects with the "Soldier" tag are currently red
    private bool isGolden = false; // Flag to track if objects with the "Target" tag are currently golden
    private bool isBlue = false; // Flag to track if objects with the "Civilian" tag are currently blue
    private bool isDead = false; // Flag to track if object with tag "Soldier" is dead
    public bool targetAlerted = false; // Flag to determine if the target has been alerted

    void Start()
    {
        originalPosition = transform.position; // Store the original position
        StartCoroutine(WobblyMovement()); // Start the wobbly movement coroutine
    }

    void Update()
    {
        if (alertedRoute.Length > 0 && targetAlerted)
        {
            Vector3 targetPosition = alertedRoute[currentEscapeRouteIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, alertedSpeed * Time.deltaTime);
            RotateTowards(targetPosition);

            if (transform.position == targetPosition)
            {
                // Increment the time since reaching the waypoint
                alertedWaipontTime += Time.deltaTime;

                // Check if the delay between waypoints has been reached
                if (alertedWaipontTime >= delayBetweenAlertedWaypoints)
                {
                    // Move to the next waypoint
                    currentEscapeRouteIndex = (currentEscapeRouteIndex + 1) % alertedRoute.Length;

                    // Reset the time since reaching the waypoint
                    alertedWaipontTime = 0f;
                }
            }
        }

        // Check if there are any waypoints to move towards
        if (waypoints.Length > 0 && !targetAlerted)
        {
            //Check if enemy is dead
            if (gameObject.tag == "DeadEnemy")
            {
                isDead = true;
            }
            if (isDead == true) return;
            // Move towards the current waypoint
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            RotateTowards(targetPosition);

            // Check if the AI has reached the current waypoint
            if (transform.position == targetPosition)
            {
                // Increment the time since reaching the waypoint
                timeSinceReachedWaypoint += Time.deltaTime;

                // Check if the delay between waypoints has been reached
                if (timeSinceReachedWaypoint >= delayBetweenWaypoints)
                {
                    // Move to the next waypoint
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                    // Reset the time since reaching the waypoint
                    timeSinceReachedWaypoint = 0f;

                }
            }
        }

        // Check for input to toggle colors and NPC names visibility
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleSoldierColors();
            ToggleTargetColors();
            ToggleCivilianColors();
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Keep the direction horizontal
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }


    IEnumerator WobblyMovement()
    {
        while (true)
        {
            // Apply random rotation for the wobbly movement
            Vector3 randomRotation = new Vector3(Random.Range(-rotationIntensity, rotationIntensity), 0f, Random.Range(-rotationIntensity, rotationIntensity));
            transform.Rotate(randomRotation);

            // Wait for a short duration before applying the next rotation
            yield return new WaitForSeconds(0.5f);
        }
    }

    

    void ToggleSoldierColors()
    {
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        GameObject[] soldierHeads = GameObject.FindGameObjectsWithTag("SoldierHead");

        foreach (GameObject soldier in soldiers)
        {
            Renderer renderer = soldier.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (isRed)
                {
                    // Change color back to original
                    renderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to red
                    renderer.material.color = Color.red;
                }
            }
        }

        foreach (GameObject head in soldierHeads)
        {
            Renderer headRenderer = head.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                if (isRed)
                {
                    // Change color back to original
                    headRenderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to red
                    headRenderer.material.color = Color.red;
                }
            }
        }

        isRed = !isRed; // Toggle the color state
    }

    void ToggleTargetColors()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        GameObject[] targetHeads = GameObject.FindGameObjectsWithTag("TargetHead");

        foreach (GameObject target in targets)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (isGolden)
                {
                    // Change color back to original
                    renderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to golden
                    renderer.material.color = Color.yellow;
                }
            }
        }

        foreach (GameObject head in targetHeads)
        {
            Renderer headRenderer = head.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                if (isGolden)
                {
                    // Change color back to original
                    headRenderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to golden
                    headRenderer.material.color = Color.yellow;
                }
            }
        }

        isGolden = !isGolden; // Toggle the color state
    }



    void ToggleCivilianColors()
    {
        GameObject[] civilians = GameObject.FindGameObjectsWithTag("Civilian");
        GameObject[] civilianHeads = GameObject.FindGameObjectsWithTag("CivilianHead");

        foreach (GameObject civilian in civilians)
        {
            Renderer renderer = civilian.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (isBlue)
                {
                    // Change color back to original
                    renderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to blue
                    renderer.material.color = Color.blue;
                }
            }
        }

        foreach (GameObject head in civilianHeads)
        {
            Renderer headRenderer = head.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                if (isBlue)
                {
                    // Change color back to original
                    headRenderer.material.color = Color.white; // Change this to the original color
                }
                else
                {
                    // Change color to blue
                    headRenderer.material.color = Color.blue;
                }
            }
        }

        isBlue = !isBlue; // Toggle the color state
    }




}

