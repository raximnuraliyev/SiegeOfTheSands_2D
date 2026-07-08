using UnityEngine;

public class SelfDestructEffect : MonoBehaviour
{
    public float delay = 1.5f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}