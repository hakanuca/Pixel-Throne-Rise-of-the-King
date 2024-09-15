using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneDelaySkipper : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndSkipScene());
    }

    IEnumerator WaitAndSkipScene()
    {
        yield return new WaitForSeconds(3f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
