using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 1f;
    public float currentHealth;
    [SerializeField] private EnemyHealthbar enemyHealthbar;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        enemyHealthbar.Initialize(this);
    }

    public virtual void TakeDamage(float damage)
    {
        if (CharacterMovement.Instance.isDashing) return;

        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        enemyHealthbar.UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject, 3f);
    }
}