using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f; // Health of the enemy
    private bool isAlive = true; // Flag indicating if the enemy is alive

    // Method to apply damage to the enemy
    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    // Method to handle enemy death
    void Die()
    {
        // Add any death behavior here, such as playing death animation, spawning effects, etc.
        isAlive = false;

        // Add Rigidbody component if not already present
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Apply a force to make the enemy fall backward when shot
        float forceMagnitude = 0.3f;
        rb.AddForce(-transform.forward * forceMagnitude, ForceMode.Impulse);

        // Mark the enemy as dead (for future detection mechanism)
        gameObject.tag = "DeadEnemy";

        // Optionally, destroy other components or GameObjects attached to the enemy
        // Destroy(GetComponent<Collider>());
        // Destroy(GetComponent<EnemyMovement>());
    }
}
