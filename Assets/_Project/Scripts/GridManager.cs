using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Size Settings")]
    public int columns = 9;
    public int rows = 5;

    [Header("Spacing Settings")]
    public float cellWidth = 1.0f;       // Tweak these to scale the grid size
    public float cellHeight = 1.4f;     // Made shorter to match your map's flat perspective

    [Header("Grid Offset Position")]
    public Vector3 gridCenterOffset = new Vector3(0, -1.5f, 0); // Lowers the grid into the sand lane area

    [Header("Visual Tile Prefab")]
    public GameObject tilePrefab;

    private GameObject[,] gameGrid;

    void Start()
    {
        gameGrid = new GameObject[columns, rows];
        GenerateVisibleGrid();
    }

    void GenerateVisibleGrid()
    {
        float startX = gridCenterOffset.x - ((columns - 1) * cellWidth) / 2f;
        float startY = gridCenterOffset.y - ((rows - 1) * cellHeight) / 2f;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 tilePos = new Vector3(startX + (x * cellWidth), startY + (y * cellHeight), 0);

                if (tilePrefab != null)
                {
                    GameObject spawnedTile = Instantiate(tilePrefab, tilePos, Quaternion.identity, transform);
                    spawnedTile.name = $"Tile_{x}_{y}";

                    // REMOVED the code forcing col.a = 0.1f;
                    // Now it will use whatever color/transparency you set on the prefab!

                    gameGrid[x, y] = spawnedTile;
                }
            }
        }
    }

    // Call this from your placement script later to snap soldiers perfectly
    public Vector3 GetNearestTilePosition(Vector3 worldPosition, out int gridX, out int gridY)
    {
        float startX = gridCenterOffset.x - ((columns - 1) * cellWidth) / 2f;
        float startY = gridCenterOffset.y - ((rows - 1) * cellHeight) / 2f;

        gridX = Mathf.RoundToInt((worldPosition.x - startX) / cellWidth);
        gridY = Mathf.RoundToInt((worldPosition.y - startY) / cellHeight);

        gridX = Mathf.Clamp(gridX, 0, columns - 1);
        gridY = Mathf.Clamp(gridY, 0, rows - 1);

        return new Vector3(startX + (gridX * cellWidth), startY + (gridY * cellHeight), 0);
    }
}