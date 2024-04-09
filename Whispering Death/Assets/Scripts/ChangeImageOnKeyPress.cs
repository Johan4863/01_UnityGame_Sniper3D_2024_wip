using UnityEngine;
using UnityEngine.UI;

public class ChangeImageOnKeyPress : MonoBehaviour
{
    public Sprite[] images; // Array of sprites for different images
    private int currentIndex = 0; // Index of the current image

    // Reference to the Image component
    private Image imageComponent;

    void Start()
    {
        // Get the Image component attached to this GameObject
        imageComponent = GetComponent<Image>();
    }

    void Update()
    {
        // Check if the "V" key is pressed
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Increment the current index or wrap around to 0 if at the end of the array
            currentIndex = (currentIndex + 1) % images.Length;

            // Change the image to the one at the current index
            imageComponent.sprite = images[currentIndex];
        }
    }
}
