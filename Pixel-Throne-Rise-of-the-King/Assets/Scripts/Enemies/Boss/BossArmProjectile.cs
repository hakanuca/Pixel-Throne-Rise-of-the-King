using UnityEngine;

public class BossArmProjectile : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float speed = 7f; // Speed of the projectile

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Destroy the projectile after 3 seconds
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            // Move the projectile towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}