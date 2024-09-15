using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointNew : MonoBehaviour
{
    // This method is called when the trigger collider interacts with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
