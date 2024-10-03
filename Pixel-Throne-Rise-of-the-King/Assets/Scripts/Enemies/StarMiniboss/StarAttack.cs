using UnityEngine;

public class StarAttack : StateMachineBehaviour
{
    public float increasedSpeed = 5f;
    private Boss starMiniboss;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 lastPosition;
    public ParticleSystem collisionEffect;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        starMiniboss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        starMiniboss?.LookAtPlayer();
        lastPosition = rb.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        starMiniboss?.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, increasedSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (rb.position == lastPosition)
        {
            animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Attack", true);
        }

        lastPosition = rb.position;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(collisionEffect, collision.contacts[0].point, Quaternion.identity);
        }
    }
}