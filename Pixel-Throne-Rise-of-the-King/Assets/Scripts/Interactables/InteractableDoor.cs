using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : IInteractable
{
    public Animator doorAnimator;
    private bool isPlayerInTrigger; // To track if the player is in the trigger area
    public Animator playerAnimator;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("IsOpen");
            doorAnimator.SetBool("IsTriggered", true);
            isPlayerInTrigger = true; // Set the flag when the player enters
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("IsClose");
            doorAnimator.SetBool("IsTriggered", false);
            isPlayerInTrigger = false; // Reset the flag when the player exits
            CancelInteract();
        }
    }

    // Monitor for the E key press in the Update method
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            DoorAnim();
            Interact();
        }
    }

    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(OpenDoorAndChangeScene());
    }

    private IEnumerator OpenDoorAndChangeScene()
    {
        doorAnimator.SetTrigger("IsClose");
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length + 1.5f);
        // Get the current scene index and load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    private void DoorAnim()
    {
        playerAnimator.Play("Main_Character_DoorIn");  
    }
}
