using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        LeanTween.alphaCanvas(canvasGroup, 1f, 2f);
    }
}