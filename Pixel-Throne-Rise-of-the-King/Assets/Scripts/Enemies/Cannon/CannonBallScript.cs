using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    private float giveDamage = 1.3f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(giveDamage);
            }
        }
        Destroy(gameObject);
    }
}