using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : IInteractable
{
    public string sceneName;
    public Animator doorAnimator;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("IsOpen");
            doorAnimator.SetBool("IsTriggered", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("IsClose");
            doorAnimator.SetBool("IsTriggered", false);
            CancelInteract();
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
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length + 3f);
        SceneManager.LoadScene(sceneName);
    }
}