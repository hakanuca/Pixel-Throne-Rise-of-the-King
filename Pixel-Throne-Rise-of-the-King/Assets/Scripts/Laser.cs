using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Collider2D laserCollider;
    [SerializeField] private float colliderDelay = 3f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        laserCollider = GetComponent<Collider2D>();
        laserCollider.enabled = false;
    }

    public void Activate()
    {
        laserCollider.gameObject.layer = LayerMask.NameToLayer("Player");
        animator.SetBool("isAttacking", true);
        StartCoroutine(EnableColliderAfterDelay(colliderDelay));
    }

    public void Deactivate()
    {
        laserCollider.enabled = false;
        animator.SetBool("isAttacking", false);
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        laserCollider.enabled = true;
    }
}