using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text keyCountText; // Assign this in the Inspector

    private void Update()
    {
        keyCountText.text = "Keys: " + KeyController.keyCount;
    }
}
