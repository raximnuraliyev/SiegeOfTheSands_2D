using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Base Health")]
    public int baseLives = 3;
    public TextMeshProUGUI livesTextDisplay;

    [Header("Victory Screen UI")]
    public GameObject victoryScreenPanel; // Drag UI_VictoryScreen here!

    void Start()
    {
        UpdateLivesUI();

        // Ensure the victory screen is hidden when a fresh level starts
        if (victoryScreenPanel != null)
        {
            victoryScreenPanel.SetActive(false);
        }
    }

    public void EnemyBreached()
    {
        baseLives--;
        UpdateLivesUI();

        if (baseLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        if (livesTextDisplay != null)
        {
            livesTextDisplay.text = $"Base Health: {baseLives}";
        }
    }

    public void TriggerLevelVictory()
    {
        Debug.Log("The lines held! Victory achieved!");

        if (victoryScreenPanel != null)
        {
            victoryScreenPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Unfreeze game time!

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Scn_Level_01")
        {
            SceneManager.LoadScene("Scn_Level_02");
        }
        else if (currentSceneName == "Scn_Level_02")
        {
            // Check if Level 3 exists in build settings, otherwise return to Menu
            if (Application.CanStreamedLevelBeLoaded("Scn_Level_03"))
            {
                SceneManager.LoadScene("Scn_Level_03");
            }
            else
            {
                SceneManager.LoadScene("Scn_MainMenu");
            }
        }
        else
        {
            SceneManager.LoadScene("Scn_MainMenu");
        }
    }

    void GameOver()
    {
        Debug.Log("The city walls have fallen! Restarting level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}