using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public int bossAttackDamage = 2;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                AttackBoss();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    
    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            CinemachineShake.Instance.ShakeCamera(5f, .1f);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }
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
            }
        }
    }
    
    void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}




