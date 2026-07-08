using UnityEngine;
using UnityEngine.InputSystem; // Added to support the new Input package

public class PlacementManager : MonoBehaviour
{
    private GridManager gridManager;

    [Header("Testing Prefab")]
    // Add this new tracker variable near the top of your class parameters
    [HideInInspector] public int currentUnitCost;
    public GameObject unitPrefab;

    void Start()
    {
        gridManager = Object.FindFirstObjectByType<GridManager>();
    }

    void Update()
    {
        // New Input System way to detect a left mouse click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandlePlacement();
        }
    }

    void HandlePlacement()
    {
        // New Input System way to grab the cursor position
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        int gridX, gridY;
        Vector3 targetSnapPos = gridManager.GetNearestTilePosition(mouseWorldPos, out gridX, out gridY);

        if (unitPrefab != null)
        {
            // Ask the economy manager to formally charge the player's account
            EconomyManager economy = Object.FindFirstObjectByType<EconomyManager>();
            if (economy != null && economy.SpendGold(currentUnitCost))
            {
                Instantiate(unitPrefab, targetSnapPos, Quaternion.identity);
                Debug.Log($"Deployed defender to column: {gridX}, row: {gridY}");

                // Reset placement state
                unitPrefab = null;
                currentUnitCost = 0;
            }
        }
    }
}