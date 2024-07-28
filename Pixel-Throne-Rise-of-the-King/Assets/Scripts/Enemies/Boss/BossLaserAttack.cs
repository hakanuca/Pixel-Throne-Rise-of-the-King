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
    Rigidbody2D rb;
    
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, boss.transform.position) <= attackRange)
        {
            if (!isAttacking)
            {
                if (timeSinceLastAttack <= 0)
                {
                    isAttacking = true;
                    boss.StartCoroutine(Attack());
                }
                else
                {
                    timeSinceLastAttack -= Time.deltaTime;
                }
            }
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
