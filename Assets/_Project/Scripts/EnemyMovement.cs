using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed;
    public int laneRow;

    [Header("Melee Combat Settings")]
    public float attackDamage = 20f;   // How much damage they deal per swing
    public float attackRate = 1.5f;     // Seconds between attacks

    private float attackTimer;
    private Health targetDefenderHealth; // Keeps track of who we are fighting
    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking)
        {
            HandleMeleeCombat();
        }
        else
        {
            // Only march left if we aren't currently blocked by a defender
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Breach condition
        if (transform.position.x < -7f)
        {
            LevelManager levelManager = Object.FindFirstObjectByType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.EnemyBreached();
            }

            Destroy(gameObject);
        }
    }

    void HandleMeleeCombat()
    {
        // If our target died and disappeared, resume marching!
        if (targetDefenderHealth == null)
        {
            isAttacking = false;
            return;
        }

        // Tick down our swing cooldown
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            targetDefenderHealth.TakeDamage(attackDamage);
            attackTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Look for the Health script on the defender we just ran into
        Health defenderHealth = collision.GetComponent<Health>();

        // Double check to make sure we didn't just walk into another marching enemy
        EnemyMovement isAnotherEnemy = collision.GetComponent<EnemyMovement>();

        if (defenderHealth != null && isAnotherEnemy == null)
        {
            // Stop marching and lock onto this defender target!
            targetDefenderHealth = defenderHealth;
            isAttacking = true;
            attackTimer = 0f;
            Debug.Log($"Enemy engaged in combat with {collision.gameObject.name}!");
        }
    }
}