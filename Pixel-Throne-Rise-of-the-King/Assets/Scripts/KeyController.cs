using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static int keyCount = 0; // Static to keep track of collected keys globally
    
    [SerializeField] private GameObject objectToDeactivate; // Use SerializeField for Inspector assignment

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player tag is correct
        {
            keyCount++;
            Destroy(gameObject); // Destroy the key object

            // Check if the key count has reached 10
            if (keyCount >= 10)
            {
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(false); // Deactivate the specified game object
                }
            }
        }
    }
}
