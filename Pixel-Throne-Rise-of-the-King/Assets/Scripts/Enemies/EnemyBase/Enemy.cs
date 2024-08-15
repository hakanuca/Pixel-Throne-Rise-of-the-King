using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 100f;
    public float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage");
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject,3f);
    }
}