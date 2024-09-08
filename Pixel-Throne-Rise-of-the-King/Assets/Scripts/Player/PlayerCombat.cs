using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackDamage = 1f;
    public float bossAttackDamage = 2f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    void Update()
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

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent?.TakeDamage(attackDamage);
        }
    }

    void AttackBoss()
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
            }
        }
    }

    void AttackMiniBoss()
    {
        animator.SetTrigger("Attack");
        Collider2D bossHealthCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Boss"));
        if (bossHealthCollider != null)
        {
            StarHealth starHealth = bossHealthCollider.GetComponent<StarHealth>();
            if (starHealth != null)
            {
                starHealth.TakeDamage(bossAttackDamage);
                animator.SetTrigger("Hit");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}