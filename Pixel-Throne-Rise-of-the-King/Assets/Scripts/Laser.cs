using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Collider2D laserCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        laserCollider = GetComponent<Collider2D>();
    }

    public void Activate()
    {
        laserCollider.enabled = true;
        laserCollider.gameObject.layer = LayerMask.NameToLayer("Player");
        animator.SetBool("isAttacking", true);
    }

    public void Deactivate()
    {
        laserCollider.enabled = false;
        animator.SetBool("isAttacking", false);
    }
}