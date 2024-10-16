using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackDamage = 1f;
    public float bossAttackDamage = 2f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public ParticleSystem hitEffect; // Add a reference to the particle system

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                AttackBoss();
                AttackMiniBoss();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public LayerMask GetEnemyLayers()
    {
        return enemyLayers;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent?.TakeDamage(attackDamage);
        }
    }

    private void AttackBoss()
    {
        animator.SetTrigger("Attack");
        Collider2D bossHealthCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Boss"));
        if (bossHealthCollider != null)
        {
            BossHealth bossHealth = bossHealthCollider.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(bossAttackDamage);
                animator.SetTrigger("Hit");
                if (hitEffect != null)
                {
                    hitEffect.Play(); // Play the particle effect
                }
            }
        }
    }

    private void AttackMiniBoss()
    {
        animator.SetTrigger("Attack");
        Collider2D miniBossHealthCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Boss"));
        if (miniBossHealthCollider != null)
        {
            StarHealth starHealth = miniBossHealthCollider.GetComponent<StarHealth>();
            if (starHealth != null)
            {
                starHealth.TakeDamage(bossAttackDamage);
                animator.SetTrigger("Hit");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}