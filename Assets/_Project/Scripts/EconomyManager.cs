using UnityEngine;
using TMPro; // Needed to update text on your UI layer

public class EconomyManager : MonoBehaviour
{
    [Header("Starting Economy")]
    public int currentGold = 150;
    public float goldPassiveGenerationRate = 2f; // Seconds between free gold ticks
    public int goldPassiveAmount = 10;           // Amount of gold given per tick

    [Header("UI Reference")]
    public TextMeshProUGUI goldTextDisplay; // Drag your UI Text asset here

    private float passiveTimer;

    void Start()
    {
        UpdateGoldUI();
    }

    void Update()
    {
        // Passive income generation over time
        passiveTimer += Time.deltaTime;
        if (passiveTimer >= goldPassiveGenerationRate)
        {
            AddGold(goldPassiveAmount);
            passiveTimer = 0f;
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true; // Purchase approved!
        }

        Debug.LogWarning("Not enough gold to deploy this unit!");
        return false; // Purchase denied
    }

    void UpdateGoldUI()
    {
        if (goldTextDisplay != null)
        {
            goldTextDisplay.text = $"Gold: {currentGold}";
        }
    }
}