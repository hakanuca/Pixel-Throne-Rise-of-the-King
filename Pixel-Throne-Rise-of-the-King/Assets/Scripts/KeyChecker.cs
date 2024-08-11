using UnityEngine;
using UnityEngine.UI;

public class KeyChecker : MonoBehaviour
{
    int keyCount = 0; // Static to keep track of collected keys globally

    [SerializeField] private GameObject objectToDeactivate; // GameObject to deactivate after collecting 10 keys
    [SerializeField] Text keyCountText;    
    
    void Check()
    {
        keyCountText.text = "Keys: " + keyCount;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key")) // Ensure the key tag is correct
        {
            keyCount++;
            Check();
            Destroy(other.gameObject); // Destroy the collected key object

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
