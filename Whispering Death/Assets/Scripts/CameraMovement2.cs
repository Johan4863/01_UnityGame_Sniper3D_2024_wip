using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    // Referencja do g��wnej kamery Unity
    public Camera mainCamera;
    // Tablica przechowuj�ca referencje do wszystkich kamer wirtualnych Cinemachine
    public CinemachineVirtualCamera[] virtualCameras;
    // Indeks aktualnie wybranej kamery wirtualnej
    private int currentCameraIndex = 0;

    void Start()
    {
        // Wy��cz wszystkie kamery wirtualne opr�cz pierwszej
        for (int i = 1; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Sprawd�, czy klawisz A zosta� naci�ni�ty
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Wy��cz aktualn� kamer� wirtualn�
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            // Zmniejsz indeks kamery wirtualnej (je�li jest 0, przejdzie na ostatni� kamer�)
            currentCameraIndex = (currentCameraIndex - 1 + virtualCameras.Length) % virtualCameras.Length;
            // W��cz now� aktualn� kamer� wirtualn�
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        }

        // Sprawd�, czy klawisz D zosta� naci�ni�ty
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Wy��cz aktualn� kamer� wirtualn�
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            // Zwi�ksz indeks kamery wirtualnej (je�li jest r�wny ostatniej kamery, przejdzie na pierwsz�)
            currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Length;
            // W��cz now� aktualn� kamer� wirtualn�
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
