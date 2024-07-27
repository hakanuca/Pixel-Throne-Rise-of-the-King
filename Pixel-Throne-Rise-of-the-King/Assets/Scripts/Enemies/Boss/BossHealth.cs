using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    public GameObject deathEffect;
    
    private bool isDead = false;
    
    void Die()
    {
        isDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    
    
}
