using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public Animator anim;
    private bool dead;
    private bool cooldownActive = false;

    private void Awake() 
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) 
        {
            anim.SetTrigger("Hurt");
        }
        else 
        {
            if (!dead)
            {
                anim.SetTrigger("Die");
                GetComponent<CharacterMovement>().enabled = false;
                GetComponent<PlayerCombat>().enabled = false;
                dead = true;
                StartCoroutine(ReloadSceneWithCooldown(3f));
                
           }
        }
    }

    private IEnumerator ReloadSceneWithCooldown(float cooldownTime)
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownTime);
        SceneManager.LoadScene(0);
        cooldownActive = false;
    }
}
