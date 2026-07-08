using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject guidePanel;

    void Start()
    {
        // Ensure the guide panel is closed when the main menu opens
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }
    }

    // Called by the "PLAY GAME" button
    public void OpenHowToPlayGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(true);
        }
    }

    // Called by the "I AM READY - DEPLOY!" button
    public void StartCampaignLevel1()
    {
        Time.timeScale = 1f; // Make sure time flows normally
        SceneManager.LoadScene("Scn_Level_01"); // Explicitly load Level 1!
    }

    // Optional: Quit Game button handler
    public void QuitGame()
    {
        Debug.Log("Quitting Siege of the Sands...");
        Application.Quit();
    }
}