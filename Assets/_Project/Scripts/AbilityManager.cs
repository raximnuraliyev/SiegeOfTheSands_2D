using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem; // Natively supports the new input system package

public class AbilityManager : MonoBehaviour
{
    [Header("Ability Settings")]
    public float cooldownTime = 15f;
    public int damagePerTick = 25;
    public float duration = 4f;

    [Header("UI References")]
    public Button abilityButton;
    public Image cooldownOverlay;

    private GridManager gridManager;
    private bool isSearchingForTarget = false;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        gridManager = Object.FindFirstObjectByType<GridManager>();
        if (cooldownOverlay != null) cooldownOverlay.fillAmount = 0f;
    }

    void Update()
    {
        if (isSearchingForTarget)
        {
            // Check if Left Mouse Button was clicked this frame
            if (Pointer.current != null && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                DetectTargetLane();
            }

            // Cancel targeting if Right Mouse Button was clicked
            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                isSearchingForTarget = false;
                Debug.Log("Ability targeting cancelled.");
            }
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
            }

            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                if (abilityButton != null) abilityButton.interactable = true;
            }
        }
    }

    public void StartAbilityTargeting()
    {
        if (isOnCooldown) return;

        isSearchingForTarget = true;
        Debug.Log("Click a row lane to unleash King Baldwin's Holy Fire! (Right-click to cancel)");
    }

    void DetectTargetLane()
    {
        if (Pointer.current == null || gridManager == null) return;

        // Read the mouse screen position position from the new system layout
        Vector2 mouseScreenPos = Pointer.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        int clickedRow, dummyX;
        gridManager.GetNearestTilePosition(mouseWorldPos, out dummyX, out clickedRow);

        if (clickedRow >= 0 && clickedRow < gridManager.rows)
        {
            ExecuteLaneStrike(clickedRow);
        }

        isSearchingForTarget = false;
    }

    void ExecuteLaneStrike(int targetRow)
    {
        Debug.Log($"<color=orange>Unleashing Holy Fire on Lane Row {targetRow}!</color>");

        StartCoroutine(LaneBurnRoutine(targetRow));

        isOnCooldown = true;
        cooldownTimer = cooldownTime;
        if (abilityButton != null) abilityButton.interactable = false;
    }

    IEnumerator LaneBurnRoutine(int targetRow)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            EnemyMovement[] normalEnemies = Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
            SpearmanMovement[] spearmen = Object.FindObjectsByType<SpearmanMovement>(FindObjectsSortMode.None);

            foreach (EnemyMovement enemy in normalEnemies)
            {
                if (enemy != null && enemy.laneRow == targetRow)
                {
                    Health h = enemy.GetComponent<Health>();
                    if (h != null) h.TakeDamage(damagePerTick);
                }
            }

            foreach (SpearmanMovement spear in spearmen)
            {
                if (spear != null && spear.laneRow == targetRow)
                {
                    Health h = spear.GetComponent<Health>();
                    if (h != null) h.TakeDamage(damagePerTick);
                }
            }

            yield return new WaitForSeconds(0.5f);
            elapsed += 0.5f;
        }
    }
}