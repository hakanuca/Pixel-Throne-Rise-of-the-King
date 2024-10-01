using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public bool canTakeDamage = true;
    public float speed = 2f; // Movement speed
    public float attackRange = 1.5f; // Range within which the boss will attack
    public int attackDamage = 10; // Damage dealt to the player
    private Animator animator;
    private Health playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }

    public void DisableDamage()
    {
        canTakeDamage = false;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (player.position.x > transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (player.position.x < transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            animator.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void Update()
    {
        LookAtPlayer();
        MoveTowardsPlayer();

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }
}