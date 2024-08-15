using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpdateHealthbar(float currentHealth)
    {
        healthBar.value = currentHealth;
    }
}