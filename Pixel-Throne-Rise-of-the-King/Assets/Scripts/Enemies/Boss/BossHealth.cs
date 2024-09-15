using System;
using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    #region Variables/Reference

    public Animator animator;
    public float maxHealth = 4f;
    public float currentHealth;
    private bool isInvulnerable = false;
    [SerializeField] private EnemyHealthbar enemyHealthbar;
    private Action onDeath;

    #endregion

    #region Unity Event Funciton

    private void Start()
    {
        currentHealth = maxHealth;
        enemyHealthbar?.InitializeBoss(this);
    }

    #endregion

    #region Self Functions

    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;
        currentHealth -= damage;
        animator.SetTrigger("Glowing");
        enemyHealthbar.UpdateBossHealthbar();
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
        onDeath?.Invoke();

        Destroy(gameObject, 3f);
    }

    public void OnDeath(Action action)
    {
        onDeath = action;
    }
    
    #endregion

    #region Enum

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    #endregion
}
