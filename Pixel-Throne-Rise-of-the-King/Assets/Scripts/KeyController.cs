using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static int keyCount = 0; // Static to keep track of collected keys globally

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the player tag is set correctly
        {
            keyCount++;
            Destroy(gameObject); // Destroy the key object
        }
    }
}
