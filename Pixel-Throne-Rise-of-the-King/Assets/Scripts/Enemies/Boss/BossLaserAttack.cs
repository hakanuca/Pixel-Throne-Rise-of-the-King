using System.Collections;
using UnityEngine;

public class BossLaserAttack : StateMachineBehaviour
{
    public GameObject laserPrefab;
    public float timeBetweenAttacks = 3f;
    public float attackDuration = 1f;
    public float attackRange = 5f;

    private Transform player;
    private bool isAttacking;
    private float timeSinceLastAttack;
    private Boss boss;
    private Transform bossTransform;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 direction = player.position - bossTransform.position;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, attackRange, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            isAttacking = true;
            boss.LookAtPlayer();
            animator.SetTrigger("Laser");
            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Laser");
    }
}
