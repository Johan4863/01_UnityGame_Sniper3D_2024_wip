using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Tablica kamer
    public int[] startingCameraIndexes; // Tablica indeksów kamery rozpoczynaj¹cej grê
    private int currentCameraIndex = 0; // Indeks aktualnej kamery

    void Start()
    {
        // Aktywujemy kamery zgodnie z indeksami startingCameraIndexes
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i < startingCameraIndexes.Length)
                SwitchCamera(i, startingCameraIndexes[i]);
            else
                SwitchCamera(i, 0); // Domyœlnie aktywujemy pierwsz¹ kamerê, jeœli brak indeksu
        }
    }

    void Update()
    {
        // Prze³¹czanie kamer w zale¿noœci od wciœniêtych klawiszy
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Prze³¹cz do poprzedniej kamery
            currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;
            SwitchCamera(currentCameraIndex, currentCameraIndex);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Prze³¹cz do nastêpnej kamery
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            SwitchCamera(currentCameraIndex, currentCameraIndex);
        }
    }

    // Metoda do wy³¹czania wszystkich kamer, z wyj¹tkiem kamery o okreœlonym indeksie
    void SwitchCamera(int cameraIndex, int newIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == cameraIndex)
                cameras[i].gameObject.SetActive(true); // W³¹czamy wybran¹ kamerê
            else
                cameras[i].gameObject.SetActive(false); // Wy³¹czamy pozosta³e kamery
        }
    }
}