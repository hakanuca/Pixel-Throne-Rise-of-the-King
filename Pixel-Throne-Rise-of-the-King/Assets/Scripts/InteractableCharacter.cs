using UnityEngine;

public class InteractableCharacter : IInteractable
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInteract();
        }
    }
    protected override void Interact()
    {
        base.Interact();
    }

    protected override void CancelInteract()
    {
        base.CancelInteract();
    }
}