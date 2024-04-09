using UnityEngine;

public class NPCNameFacingCamera : MonoBehaviour
{
    void Update()
    {
        // Find the active camera
        Camera activeCamera = Camera.main;
        if (activeCamera != null)
        {
            // Make the NPC name face the active camera
            transform.LookAt(transform.position + activeCamera.transform.rotation * Vector3.forward, activeCamera.transform.rotation * Vector3.up);
        }
    }
}
