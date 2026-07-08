using UnityEngine;

public class SpearmanMovement : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public int laneRow;

    [Header("Spear Combat Settings")]
    public float attackDamage = 15f;
    public float attackRate = 1.2f;
    public float strikeRange = 1.5f; // Long enough to hit a defender one tile away!

    private float attackTimer;
    private Health targetDefenderHealth;
    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking)
        {
            HandleSpearCombat();
        }
        else
        {
            // Check ahead to see if a defender is sitting one tile in front of us
            LookForDefendersAhead();

            if (!isAttacking)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
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

    void LookForDefendersAhead()
    {
        // 1. Create a LayerMask that ignores the "Ignore Raycast" layer, 
        // OR we can just turn off queries hitting triggers globally for this shot.
        // The cleanest way in Unity 2D is to tell the ray to start slightly in front of the enemy,
        // or tell Unity physics to not hit its own starting collider:

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, strikeRange);

        foreach (RaycastHit2D hit in hits)
        {
            // If the hit object is regular sand grid or OURSELVES, skip it!
            if (hit.collider.gameObject == this.gameObject)
                continue;

            Health defender = hit.collider.GetComponent<Health>();
            EnemyMovement isAnotherEnemy = hit.collider.GetComponent<EnemyMovement>();
            SpearmanMovement isAnotherSpearman = hit.collider.GetComponent<SpearmanMovement>();

            // If we find a valid defender who isn't a teammate
            if (defender != null && isAnotherEnemy == null && isAnotherSpearman == null)
            {
                targetDefenderHealth = defender;
                isAttacking = true;
                attackTimer = 0f;
                Debug.Log($"Spearman successfully locked onto {hit.collider.gameObject.name} from a distance!");
                break; // Stop looking, we found our target
            }
        }
    }

    void HandleSpearCombat()
    {
        if (targetDefenderHealth == null)
        {
            isAttacking = false;
            return;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            targetDefenderHealth.TakeDamage(attackDamage);
            attackTimer = 0f;
        }
    }
}