using UnityEngine;

public class StarAttack : StateMachineBehaviour
{
    public float increasedSpeed = 5f;
    public float attackRange = 3f; 
    private Boss starMiniboss;
    private Transform player;
    private Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        starMiniboss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        starMiniboss.LookAtPlayer();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, increasedSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        
        float distanceToPlayer = Vector2.Distance(rb.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}