using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 4;
    public int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Glowing");

        if (currentHealth <= 2)
        {
            GetComponent<Animator>().SetBool("Defence", true);
        }
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject,3f);
    }

}
