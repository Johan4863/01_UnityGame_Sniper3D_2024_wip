using UnityEngine;

public class CivilianAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista punktów docelowych
    public float runSpeed = 10f; // Szybkoœæ biegu

    public bool canRun = false; // Zmienna okreœlaj¹ca, czy przeciwnik mo¿e biec

    void Update()
    {
        if (canRun && waypoints.Length > 0)
        {
            // ZnajdŸ najbli¿szy punkt docelowy
            Transform nearestWaypoint = FindNearestWaypoint();

            // Przemieœæ siê w kierunku najbli¿szego punktu docelowego
            if (nearestWaypoint != null)
            {
                // Obracaj postaæ w kierunku najbli¿szego punktu docelowego
                transform.LookAt(nearestWaypoint);

                // Przemieœæ siê w kierunku najbli¿szego punktu docelowego
                transform.position = Vector3.MoveTowards(transform.position, nearestWaypoint.position, runSpeed * Time.deltaTime);
            }
        }
    }

    Transform FindNearestWaypoint()
    {
        Transform nearestWaypoint = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform waypoint in waypoints)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoint.position);
            if (distanceToWaypoint < shortestDistance)
            {
                shortestDistance = distanceToWaypoint;
                nearestWaypoint = waypoint;
            }
        }

        return nearestWaypoint;
    }

    public void SetCanRun(bool value)
    {
        canRun = value;
    }
}
