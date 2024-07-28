using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public Health playerHealth;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
