using UnityEngine;

public class CivilianAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista punkt�w docelowych
    public float runSpeed = 10f; // Szybko�� biegu

    public bool canRun = false; // Zmienna okre�laj�ca, czy przeciwnik mo�e biec

    void Update()
    {
        if (canRun && waypoints.Length > 0)
        {
            // Znajd� najbli�szy punkt docelowy
            Transform nearestWaypoint = FindNearestWaypoint();

            // Przemie�� si� w kierunku najbli�szego punktu docelowego
            if (nearestWaypoint != null)
            {
                // Obracaj posta� w kierunku najbli�szego punktu docelowego
                transform.LookAt(nearestWaypoint);

                // Przemie�� si� w kierunku najbli�szego punktu docelowego
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
