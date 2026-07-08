using System.Collections;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [Header("Economy Settings")]
    public int goldProductionAmount = 25; // Generates a nice lump sum of gold
    public float productionInterval = 5f;  // Every 5 seconds

    private EconomyManager economyManager;

    void Start()
    {
        economyManager = Object.FindFirstObjectByType<EconomyManager>();

        // Start the generation loop
        StartCoroutine(GenerateResourcesPool());
    }

    IEnumerator GenerateResourcesPool()
    {
        while (true)
        {
            yield return new WaitForSeconds(productionInterval);

            if (economyManager != null)
            {
                economyManager.AddGold(goldProductionAmount);
                Debug.Log($"{gameObject.name} generated {goldProductionAmount} gold!");

                // Visual feedback tip: You could trigger a small sparkle animation or color flash here later!
            }
        }
    }
}