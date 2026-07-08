using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Visual Effects")]
    public GameObject damagePopupPrefab; // Drag Prf_DamagePopup here in the inspector

    [Header("Death Effects")]
    public GameObject deathParticlePrefab; // Drag Prf_DeathSmoke here!

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took damage. Remaining: {currentHealth}");

        // 1. RESTORED: Trigger the red damage flash effect
        DamageFlash flasher = GetComponent<DamageFlash>();
        if (flasher != null)
        {
            flasher.CallFlash();
        }

        // 2. RESTORED: Spawn the floating damage number on the canvas
        if (damagePopupPrefab != null)
        {
            Canvas canvas = Object.FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                GameObject popup = Instantiate(damagePopupPrefab, canvas.transform);

                // Position it slightly above the unit's head
                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0f, 0.6f, 0f));
                popup.transform.position = screenPos;

                FloatingText floatingText = popup.GetComponent<FloatingText>();
                if (floatingText != null)
                {
                    // Yellow numbers for enemies, red for defenders
                    Color textColor = (GetComponent<EnemyMovement>() != null || GetComponent<SpearmanMovement>() != null)
                        ? Color.yellow
                        : Color.red;

                    floatingText.SetText(amount.ToString(), textColor);
                }
            }
        }

        // 3. Handle unit expiration and spawn the death particles
        if (currentHealth <= 0)
        {
            if (deathParticlePrefab != null)
            {
                Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}