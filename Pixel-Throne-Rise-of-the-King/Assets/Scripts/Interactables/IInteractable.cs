using System;
using UnityEngine;
using UnityEngine.Events;

public class IInteractable : MonoBehaviour
{
    public UnityEvent onInteract;
    public UnityEvent onCancelInteract;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

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

    protected virtual void Interact()
    {
        onInteract?.Invoke();
    }
    
    protected virtual void CancelInteract()
    {
        onCancelInteract?.Invoke();
    }
}