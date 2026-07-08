using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This lets us customize waves directly inside the Unity Inspector layout
[System.Serializable]
public class WaveConfig
{
    public string waveName;
    public GameObject waveEnemyPrefab;
    public int totalEnemiesInWave = 5;
    public float timeBetweenSpawns = 3f;
    public float enemyMinSpeed = 0.5f;
    public float enemyMaxSpeed = 1.2f;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;

    [Header("Wave Progression Setup")]
    public List<WaveConfig> levelWaves = new List<WaveConfig>();
    public float timeBetweenWaves = 10f; // Intermission break for setup

    private GridManager gridManager;
    private int currentWaveIndex = 0;
    private bool levelComplete = false;

    void Start()
    {
        gridManager = Object.FindFirstObjectByType<GridManager>();

        if (levelWaves.Count > 0)
        {
            StartCoroutine(LevelWaveRoutine());
        }
        else
        {
            Debug.LogWarning("No waves configured in the Enemy Spawner component!");
        }
    }

    IEnumerator LevelWaveRoutine()
    {
        while (currentWaveIndex < levelWaves.Count)
        {
            WaveConfig currentWave = levelWaves[currentWaveIndex];
            Debug.Log($"<color=cyan>Incoming Wave: {currentWave.waveName}!</color>");

            // Spawn every enemy allocated for this specific wave
            for (int i = 0; i < currentWave.totalEnemiesInWave; i++)
            {
                SpawnEnemy(currentWave.waveEnemyPrefab, currentWave.enemyMinSpeed, currentWave.enemyMaxSpeed);
                yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
            }

            currentWaveIndex++;

            // If there's another wave coming, give the player an intermission break to build economy
            if (currentWaveIndex < levelWaves.Count)
            {
                Debug.Log($"Wave completed! Next wave starts in {timeBetweenWaves} seconds.");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        // Find this line at the bottom of LevelWaveRoutine() inside EnemySpawner.cs:
        levelComplete = true;

        // Update this block to report the win directly to your manager:
        LevelManager levelManager = Object.FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.TriggerLevelVictory();
        }
    }

    void SpawnEnemy(GameObject prefabToSpawn, float minSpeed, float maxSpeed)
    {
        if (prefabToSpawn == null || gridManager == null) return;

        // 1. Pick a random lane row index (0 to 4)
        int randomRow = Random.Range(0, gridManager.rows);

        // 2. Mathematically map the starting position off-screen right
        int dummyX;
        Vector3 spawnPos = gridManager.GetNearestTilePosition(Vector3.zero, out dummyX, out dummyX);
        spawnPos.x = 6.5f;

        float startY = gridManager.gridCenterOffset.y - ((gridManager.rows - 1) * gridManager.cellHeight) / 2f;
        spawnPos.y = startY + (randomRow * gridManager.cellHeight);

        // 3. Instantiate the passed-in enemy prefab parameter
        GameObject enemyInstance = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // Check for standard movement component
        EnemyMovement movement = enemyInstance.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.moveSpeed = Random.Range(minSpeed, maxSpeed);
            movement.laneRow = randomRow;
        }

        // Add this check for your spearman component!
        SpearmanMovement spearmanMovement = enemyInstance.GetComponent<SpearmanMovement>();
        if (spearmanMovement != null)
        {
            spearmanMovement.moveSpeed = Random.Range(minSpeed, maxSpeed);
            spearmanMovement.laneRow = randomRow;
        }
    }
}