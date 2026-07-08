using UnityEngine;

public class LeaderAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public GameObject flaskPrefab;
    public float cooldownTime = 4f;     // Longer cooldown because it's a powerful weapon
    public float targetDetectionRange = 6f;

    private float cooldownTimer;
    private int myLaneRow = -1;

    void Start()
    {
        GridManager grid = Object.FindFirstObjectByType<GridManager>();
        if (grid != null)
        {
            int dummyX;
            grid.GetNearestTilePosition(transform.position, out dummyX, out myLaneRow);
        }
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldownTime)
        {
            if (IsEnemyApproaching())
            {
                ThrowFlask();
                cooldownTimer = 0f;
            }
        }
    }

    bool IsEnemyApproaching()
    {
        EnemyMovement[] enemies = Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (EnemyMovement enemy in enemies)
        {
            if (enemy.laneRow == myLaneRow && enemy.transform.position.x > transform.position.x)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= targetDetectionRange)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void ThrowFlask()
    {
        if (flaskPrefab != null)
        {
            Vector3 throwPos = transform.position + new Vector3(0.4f, 0.2f, 0f);
            Instantiate(flaskPrefab, throwPos, Quaternion.identity);
            Debug.Log("King Baldwin unleashed Greek Fire!");
        }
    }
}