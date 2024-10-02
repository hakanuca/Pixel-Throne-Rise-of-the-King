using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public bool canTakeDamage = true;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 2f; // Cooldown duration in seconds
    private bool canAttack = true; // Flag to check if the boss can attack
    private bool isAttacking = false; // Flag to control movement during attack
    private Animator animator;
    private Health playerHealth;
    public ParticleSystem attackEffect; // Particle system for attack effect
    private BossHealth bossHealth;

    // New variables for spawning
    public GameObject spawnPrefab; // Prefab to spawn
    public Vector2 spawnAreaMin; // Minimum coordinates of the spawn area
    public Vector2 spawnAreaMax; // Maximum coordinates of the spawn area

    private void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    public void DisableDamage()
    {
        bossHealth.SetInvulnerable(true);
    }

    public void EnableDamage()
    {
        bossHealth.SetInvulnerable(false);
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
        if (player != null && !isAttacking)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        if (playerHealth != null && canAttack)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
            attackEffect.Play(); 
            
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        isAttacking = false;
    }

    private bool IsDefenseAnimationPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Defense");
    }

    private void Update()
    {
        if (!IsDefenseAnimationPlaying())
        {
            LookAtPlayer();
            MoveTowardsPlayer();

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }
    public IEnumerator SpawnObjectRoutine()
    {
        while (true)
        {
            SpawnObjectInArea();
            yield return new WaitForSeconds(5f);
        }
    }
    // New method to spawn a game object in a given area
    public void SpawnObjectInArea()
    {
        if (spawnPrefab != null)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector2 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawWireCube(center, size);
    }
}