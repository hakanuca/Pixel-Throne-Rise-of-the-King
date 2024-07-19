using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; 
    [SerializeField] private RectTransform visiblePosition;

    [SerializeField] private GameObject menu1;
    [SerializeField] private RectTransform visiblePositionMenu1;

    [SerializeField] private GameObject menu2;
    [SerializeField] private RectTransform visiblePositionMenu2;

    [SerializeField] private GameObject menu3;
    [SerializeField] private RectTransform visiblePositionMenu3;

    [SerializeField] private GameObject menu4;
    [SerializeField] private RectTransform visiblePositionMenu4;

    [SerializeField] private GameObject menu5;
    [SerializeField] private RectTransform visiblePositionMenu5;

    void Start()
    {
        LeanTween.delayedCall(1f, () => LeanTween.move(targetObject, visiblePosition.position, 1f));
        LeanTween.delayedCall(2f, () => LeanTween.move(menu1, visiblePositionMenu1.position, 1f));
        LeanTween.delayedCall(3f, () => LeanTween.move(menu2, visiblePositionMenu2.position, 1f));
        LeanTween.delayedCall(4f, () => LeanTween.move(menu3, visiblePositionMenu3.position, 1f));
        LeanTween.delayedCall(5f, () => LeanTween.move(menu4, visiblePositionMenu4.position, 1f));
        LeanTween.delayedCall(6f, () => LeanTween.move(menu5, visiblePositionMenu5.position, 1f));
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
