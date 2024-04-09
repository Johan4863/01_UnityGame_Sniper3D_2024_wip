using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Tablica kamer
    public int[] startingCameraIndexes; // Tablica indeks�w kamery rozpoczynaj�cej gr�
    private int currentCameraIndex = 0; // Indeks aktualnej kamery

    void Start()
    {
        // Aktywujemy kamery zgodnie z indeksami startingCameraIndexes
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i < startingCameraIndexes.Length)
                SwitchCamera(i, startingCameraIndexes[i]);
            else
                SwitchCamera(i, 0); // Domy�lnie aktywujemy pierwsz� kamer�, je�li brak indeksu
        }
    }

    void Update()
    {
        // Prze��czanie kamer w zale�no�ci od wci�ni�tych klawiszy
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Prze��cz do poprzedniej kamery
            currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;
            SwitchCamera(currentCameraIndex, currentCameraIndex);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Prze��cz do nast�pnej kamery
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            SwitchCamera(currentCameraIndex, currentCameraIndex);
        }
    }

    // Metoda do wy��czania wszystkich kamer, z wyj�tkiem kamery o okre�lonym indeksie
    void SwitchCamera(int cameraIndex, int newIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == cameraIndex)
                cameras[i].gameObject.SetActive(true); // W��czamy wybran� kamer�
            else
                cameras[i].gameObject.SetActive(false); // Wy��czamy pozosta�e kamery
        }
    }
}