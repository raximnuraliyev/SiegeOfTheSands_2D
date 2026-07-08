using UnityEngine;

public class GreekFireFlask : MonoBehaviour
{
    public float speed = 4f;
    public float damage = 50f; // High damage area-of-effect
    public GameObject explosionVFXPrefab; // Handled in future animation steps

    void Update()
    {
        // Fly straight right across the lane
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Clean up if it misses everything
        if (transform.position.x > 8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it impacts an enemy, unleash the Greek Fire explosion!
        if (collision.CompareTag("Enemy") || collision.GetComponent<EnemyMovement>() != null)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Spawn the explosion VFX if available
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }

        // Area of Effect Damage: Find all nearby enemies in a small blast circle
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, 1.2f);
        foreach (Collider2D obj in hitObjects)
        { // <-- This brace was missing! Fixed now.
            Health enemyHealth = obj.GetComponent<Health>();
            if (enemyHealth != null && obj.GetComponent<EnemyMovement>() != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Destroy the flask itself
        Destroy(gameObject);
    }

    // Draw the blast radius in the Scene View editor window so you can visualize it
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }
}