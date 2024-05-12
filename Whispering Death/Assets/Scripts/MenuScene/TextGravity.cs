using UnityEngine;

public class TextGravity : MonoBehaviour
{
    Rigidbody rb;
    bool gravityActivated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (!gravityActivated && Input.GetKeyDown(KeyCode.Space))
        {
            rb.useGravity = true;
            gravityActivated = true;
            Destroy(gameObject, 2f);
        }
    }
}
