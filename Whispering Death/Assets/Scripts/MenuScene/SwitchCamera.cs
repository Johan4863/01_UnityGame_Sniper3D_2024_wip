using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera visualCamera;
    bool switched = false;

    void Update()
    {
        if (!switched && Input.GetKeyDown(KeyCode.Space))
        {
            switched = true;
            Invoke("SwitchToVisualCamera", 2f);
        }
    }

    void SwitchToVisualCamera()
    {
        if (visualCamera != null)
        {
            Camera.main.enabled = false;
            visualCamera.enabled = true;
        }
    }
}
