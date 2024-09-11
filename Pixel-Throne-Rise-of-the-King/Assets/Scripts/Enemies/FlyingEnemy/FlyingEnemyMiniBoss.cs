using UnityEngine;
using System.Collections;

public class FlyingEnemyMiniBoss : Enemy
{
    public GameObject player;
    public float speed;
    public int damage;
    public float distanceBetween;
    private float distance;

    [SerializeField] private GameObject fireballPrefab; // Reference to the fireball prefab
    public float fireballSpeed;
    public float fireballCooldown = 3f; // Updated cooldown to 3 seconds
    private float lastFireballTime;

    protected override void Start()
    {
        base.Start();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < distanceBetween)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (Time.time > lastFireballTime + fireballCooldown)
            {
                ThrowFireball();
                lastFireballTime = Time.time;
            }
        }
    }

    void ThrowFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        StartCoroutine(FollowPlayer(fireball));
        Destroy(fireball, 3f); // Destroy fireball after 3 seconds
    }

    IEnumerator FollowPlayer(GameObject fireball)
    {
        while (fireball != null)
        {
            Vector2 direction = (player.transform.position - fireball.transform.position).normalized;
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            rb.velocity = direction * fireballSpeed;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}