using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthBarFill; // Reference to the fill image of the slider
    private Enemy enemy;
    private BossHealth boss;

    // You can define color thresholds for healthy, medium, and critical health
    [SerializeField] private Color healthyColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color criticalHealthColor = Color.red;

    public void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
        SetMaxHealth();
    }

    public void InitializeBoss(BossHealth boss)
    {
        this.boss = boss;
        SetMaxBossHealth();
    }

    public void SetMaxHealth()
    {
        healthBar.maxValue = enemy.maxHealth;
        healthBar.value = enemy.currentHealth;
        UpdateHealthbarColor(); // Update color when setting max health
    }

    public void SetMaxBossHealth()
    {
        healthBar.maxValue = boss.maxHealth;
        healthBar.value = boss.currentHealth;
        UpdateBossHealthbarColor(); // Update color when setting max boss health
    }

    public void UpdateHealthbar()
    {
        healthBar.value = enemy.currentHealth;
        UpdateHealthbarColor(); // Update color based on current health
        healthBar.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }

    public void UpdateBossHealthbar()
    {
        healthBar.value = boss.currentHealth;
        UpdateBossHealthbarColor(); // Update color based on boss health
        healthBar.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }

    // Update health bar color for normal enemy
    private void UpdateHealthbarColor()
    {
        float healthPercentage = healthBar.value / healthBar.maxValue;

        if (healthPercentage > 0.5f)
        {
            healthBarFill.color = Color.Lerp(mediumHealthColor, healthyColor, (healthPercentage - 0.5f) * 2);
        }
        else
        {
            healthBarFill.color = Color.Lerp(criticalHealthColor, mediumHealthColor, healthPercentage * 2);
        }
    }

    // Update health bar color for boss
    private void UpdateBossHealthbarColor()
    {
        float healthPercentage = healthBar.value / healthBar.maxValue;

        if (healthPercentage > 0.5f)
        {
            healthBarFill.color = Color.Lerp(mediumHealthColor, healthyColor, (healthPercentage - 0.5f) * 2);
        }
        else
        {
            healthBarFill.color = Color.Lerp(criticalHealthColor, mediumHealthColor, healthPercentage * 2);
        }
    }
}
