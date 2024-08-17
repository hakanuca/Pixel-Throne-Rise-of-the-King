using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private static BossHealth _bossHealth;

    public void Initialize(Enemy enemy)
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        healthBar.maxValue = _bossHealth.maxHealth;
    }

    public void UpdateHealthbar()
    {
        healthBar.value = _bossHealth.currentHealth;
        healthBar.transform.rotation = Quaternion.Euler (0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }
}