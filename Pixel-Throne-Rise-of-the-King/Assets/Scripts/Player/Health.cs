using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public Animator anim;
    private bool dead;
    private bool cooldownActive = false;
    AudioManager audioManager;
    private bool IsAvailable = true;
    private int CooldownDuration = 1;
    [SerializeField] private bool isInvincible;

    private void Awake()
    {
        currentHealth = startingHealth;
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isInvincible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInvincible = !isInvincible;
        }
    }

    public void TakeDamage(float _damage)
    {
        if (IsAvailable == false || isInvincible)
        {
            return;
        }

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
                //audioManager.PlaySFX(audioManager.death);
                GetComponent<CharacterMovement>().enabled = false;
                GetComponent<PlayerCombat>().enabled = false;
                dead = true;
                //StartCoroutine(ReloadSceneWithCooldown(3f));
            }
        }
        StartCoroutine(StartCooldown());
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator ReloadSceneWithCooldown(float cooldownTime)
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownTime);
        SceneManager.LoadScene(0);
        cooldownActive = false;
    }

    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }
}