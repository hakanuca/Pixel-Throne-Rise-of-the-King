using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public float delay = 0.1f;
    [Multiline]
    public string yazi;
  
    Text thisText;

    private void Start()
    {
        thisText = GetComponent<Text>();
        StartCoroutine(TypeWrite());
    }

    IEnumerator TypeWrite()
    {
        string[] sentences = yazi.Split('.');

        foreach (string sentence in sentences)
        {
            foreach (char character in sentence)
            {
                thisText.text += character;
                yield return new WaitForSeconds(delay);
            }

            thisText.text += ".";
        }
    }
}
