using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 20f;

    void Update()
    {
        // Fly straight right down the lane
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Clean up if it flies off-screen
        if (transform.position.x > 8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit an enemy
        Health enemyHealth = collision.GetComponent<Health>();
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();

        if (enemyHealth != null && enemy != null)
        {
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject); // Destroy the arrow upon impact
        }
    }
}