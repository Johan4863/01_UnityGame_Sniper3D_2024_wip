using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints the AI will move to
    public Transform[] alertedRoute; // Array to hold the waypoints the AI will move to when alerted
    public float speed = 5f; // Speed at which the AI moves
    public float alertedSpeed = 10f; //Speed of AI when got alerted
    public float delayBetweenWaypoints = 1f; // Delay between reaching each waypoint
    public float delayBetweenAlertedWaypoints = 0.6f;
    public float rotationIntensity = 30f; // Intensity of the wobbly movement
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private int currentEscapeRouteIndex = 0; // Index of the current waypoint when alerted
    private float timeSinceReachedWaypoint = 0f; // Time elapsed since reaching the current waypoint
    private float alertedWaipontTime = 0f;
    private Vector3 originalPosition; // Original position of the AI
    private bool isRed = false; // Flag to track if objects with the "Soldier" tag are currently red
    private bool isGolden = false; // Flag to track if objects with the "Target" tag are currently golden
    private bool isBlue = false; 
    private bool isDead = false; //Flag to track if object with tag "Soldier" is dead
    public bool targetAlerted = false;
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
            if(gameObject.tag == "DeadEnemy")
            {
                isDead = true;
            }
            if (isDead == true) return;
            // Move towards the current waypoint
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

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
        isRed = !isRed; // Toggle the color state
    }

   

    void ToggleTargetColors()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
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
        isGolden = !isGolden; // Toggle the color state
    }

   


    void ToggleCivilianColors()
    {
        GameObject[] civilians = GameObject.FindGameObjectsWithTag("Civilian");
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
        isBlue = !isBlue; // Toggle the color state
    }

    

}

