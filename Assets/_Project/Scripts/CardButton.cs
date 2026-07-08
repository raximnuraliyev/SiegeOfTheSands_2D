using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    [Header("Card Asset Config")]
    public GameObject unitPrefab;
    public int goldCost = 50; // Set the individual cost per unit here!

    private PlacementManager placementManager;
    private EconomyManager economyManager;
    private Button buttonComponent;

    void Start()
    {
        placementManager = Object.FindFirstObjectByType<PlacementManager>();
        economyManager = Object.FindFirstObjectByType<EconomyManager>();
        buttonComponent = GetComponent<Button>();

        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(SelectUnit);
        }
    }

    void SelectUnit()
    {
        if (placementManager != null && economyManager != null && unitPrefab != null)
        {
            // Check if the player can afford this card right now
            if (economyManager.currentGold >= goldCost)
            {
                placementManager.unitPrefab = unitPrefab;
                // Pass the cost parameter down to the placement manager so it knows what to charge upon placement
                placementManager.currentUnitCost = goldCost;
                Debug.Log($"Card selected: Ready to deploy {unitPrefab.name} for {goldCost} gold!");
            }
            else
            {
                Debug.LogWarning("Insufficient funds to prime this unit!");
            }
        }
    }
}