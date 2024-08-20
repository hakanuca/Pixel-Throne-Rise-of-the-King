using UnityEngine;

public class InteractableCharacter : IInteractable
{
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
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