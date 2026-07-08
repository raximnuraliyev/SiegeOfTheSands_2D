using UnityEngine;
using TMPro; // Crucial for TextMeshProUGUI

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float lifetime = 0.8f;
    public Color fadeColor = Color.white;

    private TextMeshProUGUI textMeshUI; // Changed to UGUI
    private float timer;

    void Awake()
    {
        textMeshUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text, Color color)
    {
        if (textMeshUI == null) textMeshUI = GetComponent<TextMeshProUGUI>();

        if (textMeshUI != null)
        {
            textMeshUI.text = text;
            fadeColor = color;
            textMeshUI.color = fadeColor;
        }
    }

    void Update()
    {
        // Float upward
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Fade out alpha channels cleanly
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);

        if (textMeshUI != null)
        {
            fadeColor.a = alpha;
            textMeshUI.color = fadeColor;
        }

        // Self-destruct when expired
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}