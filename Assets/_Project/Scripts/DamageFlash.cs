using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Color flashColor = Color.red;
    public float flashDuration = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void CallFlash()
    {
        // If we are already flashing from a previous hit, stop it and start fresh
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        if (gameObject.activeInHierarchy && spriteRenderer != null)
        {
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}