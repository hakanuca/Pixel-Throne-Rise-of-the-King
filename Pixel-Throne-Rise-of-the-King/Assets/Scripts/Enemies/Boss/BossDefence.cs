using System.Collections;
using UnityEngine;

public class BossDefence : MonoBehaviour
{
    public float invincibleTime = 2f;
    private bool isInvincible = false;
    
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(Invincible());
            var currentHealth = damage;
            //healthBar.SetHealth(currentHealth);
        }
    }
    
    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}
