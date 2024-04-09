using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    // Referencja do g³ównej kamery Unity
    public Camera mainCamera;
    // Tablica przechowuj¹ca referencje do wszystkich kamer wirtualnych Cinemachine
    public CinemachineVirtualCamera[] virtualCameras;
    // Indeks aktualnie wybranej kamery wirtualnej
    private int currentCameraIndex = 0;

    void Start()
    {
        // Wy³¹cz wszystkie kamery wirtualne oprócz pierwszej
        for (int i = 1; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // SprawdŸ, czy klawisz A zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Wy³¹cz aktualn¹ kamerê wirtualn¹
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            // Zmniejsz indeks kamery wirtualnej (jeœli jest 0, przejdzie na ostatni¹ kamerê)
            currentCameraIndex = (currentCameraIndex - 1 + virtualCameras.Length) % virtualCameras.Length;
            // W³¹cz now¹ aktualn¹ kamerê wirtualn¹
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        }

        // SprawdŸ, czy klawisz D zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Wy³¹cz aktualn¹ kamerê wirtualn¹
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            // Zwiêksz indeks kamery wirtualnej (jeœli jest równy ostatniej kamery, przejdzie na pierwsz¹)
            currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Length;
            // W³¹cz now¹ aktualn¹ kamerê wirtualn¹
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
