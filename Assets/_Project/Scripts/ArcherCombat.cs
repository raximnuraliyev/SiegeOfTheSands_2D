using UnityEngine;

public class ArcherCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public GameObject arrowPrefab;
    public float fireRate = 1.5f;
    public float detectionRange = 8f;

    private float fireTimer;
    private int myLaneRow = -1;

    void Start()
    {
        GridManager grid = Object.FindFirstObjectByType<GridManager>();
        if (grid != null)
        {
            int dummyX;
            grid.GetNearestTilePosition(transform.position, out dummyX, out myLaneRow);
            Debug.Log($"{gameObject.name} successfully deployed to row index: {myLaneRow}");
        }
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            if (IsEnemyInLane())
            {
                ShootArrow();
                fireTimer = 0f;
            }
        }
    }

    bool IsEnemyInLane()
    {
        EnemyMovement[] enemies = Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (EnemyMovement enemy in enemies)
        {
            if (enemy.laneRow == myLaneRow && enemy.transform.position.x > transform.position.x)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= detectionRange)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void ShootArrow()
    {
        if (arrowPrefab != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0.4f, 0f, 0f);
            Instantiate(arrowPrefab, spawnPos, Quaternion.identity);
        }
    }
}