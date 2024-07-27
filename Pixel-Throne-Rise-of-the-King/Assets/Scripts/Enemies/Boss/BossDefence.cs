using System.Collections;
using UnityEngine;

public class BossDefence : MonoBehaviour
{
    //write class that makes boss invincible for a short period of time after taking damage
    
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
