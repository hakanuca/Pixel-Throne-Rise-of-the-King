using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPos;
    public float width;
    public float height;
    public LayerMask whatIsPlayer;
    [SerializeField]
    private string sceneName;
    SceneSwitch sceneSwitch;
    private Animator anim;
    private bool cooldownActive = false;
    
    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceneSwitch>();
    }

    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPos.position, new Vector2(width,height), 0, whatIsPlayer);
        if (playerDetected == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ReloadSceneWithCooldown(1f));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }

    private IEnumerator ReloadSceneWithCooldown(float cooldownTime)
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownTime);
        sceneSwitch.SwitchScene(sceneName);
        cooldownActive = false;
    }
}
