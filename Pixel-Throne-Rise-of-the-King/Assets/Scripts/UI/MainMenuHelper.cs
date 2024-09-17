using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import this to use SceneManager

public class MainMenuHelper : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] GameObject MainMenuButtons;

    void Start()
    {
        canvasGroup = MainMenuButtons.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = MainMenuButtons.AddComponent<CanvasGroup>();
        }

        // Move the buttons to the visible position and fade them in
        LeanTween.alphaCanvas(canvasGroup, 1f, 2f).setOnComplete(OnFadeComplete);
    }

    void OnFadeComplete()
    {
        // Load the next scene (current scene + 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
