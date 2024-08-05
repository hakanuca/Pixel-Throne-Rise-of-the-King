using System.Collections;
using UnityEngine;

public class StarHealth : MonoBehaviour
{
    #region Variables/Reference

    public Animator animator;
    public int maxHealth = 4;
    public int currentHealth;
    private bool isInvulnerable = false;

    #endregion

    #region Unity Event Funciton

    private void Start()
    {
        currentHealth = maxHealth;
    }

    #endregion

    #region Self Functions
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
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

    #endregion

    #region Enum

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    #endregion
}