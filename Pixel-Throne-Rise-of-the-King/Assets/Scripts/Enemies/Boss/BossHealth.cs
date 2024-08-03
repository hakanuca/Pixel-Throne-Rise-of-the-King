using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 4;
    public int currentHealth;
    private bool isInvulnerable = false;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;
        animator.SetTrigger("Glowing");

        if (currentHealth <= 2 && currentHealth > 0)
        {
            GetComponent<Animator>().SetBool("Defense", true);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        animator.SetTrigger("Death");
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        Destroy(gameObject, 3f);
    }
    
    
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
    

}
