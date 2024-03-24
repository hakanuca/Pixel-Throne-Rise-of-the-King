using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private KnockbackOnPlayer knockback;

    private void Awake() 
    {
        currentHealth = startingHealth;
    }

    private void Start() 
    {
        knockback = GetComponent<KnockbackOnPlayer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) 
        {

        }
        else 
        {

        }


        //knockback
        knockback.CallKnocbkack
    }

    private void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1.3f);
        }
    }
}
