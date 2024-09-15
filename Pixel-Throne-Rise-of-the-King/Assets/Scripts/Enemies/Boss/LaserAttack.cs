using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] private GameObject[] lasers;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float duration = 3f;
    private float lastAttackTime;
    private bool isAttacking;

    private void Update()
    {
        if (isAttacking)
        {
            if (Time.time - lastAttackTime >= duration)
            {
                DeactivateLasers();
                isAttacking = false;
                lastAttackTime = Time.time;
            }
        }
        else
        {
            if (Time.time - lastAttackTime >= cooldown)
            {
                ActivateLasers();
                isAttacking = true;
                lastAttackTime = Time.time;
            }
        }
    }

    private void ActivateLasers()
    {
        foreach (var laser in lasers)
        {
            laser.SetActive(true);
            laser.GetComponent<Laser>().Activate();
        }
    }

    private void DeactivateLasers()
    {
        foreach (var laser in lasers)
        {
            laser.GetComponent<Laser>().Deactivate();
            laser.SetActive(false);
        }
    }
}