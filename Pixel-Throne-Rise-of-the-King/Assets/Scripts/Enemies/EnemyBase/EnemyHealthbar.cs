using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private Enemy enemy;

    public void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        healthBar.maxValue = enemy.maxHealth;
        healthBar.value = enemy.currentHealth;
    }

    public void UpdateHealthbar()
    {
        healthBar.value = enemy.currentHealth;
        healthBar.transform.rotation = Quaternion.Euler (0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }
}