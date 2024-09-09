using UnityEngine;

public class FlyingEnemyMiniBoss : Enemy
{
    public GameObject player;
    public float speed;
    public int damage;
    public float distanceBetween;
    private float distance;

    [SerializeField] private GameObject fireballPrefab; // Reference to the fireball prefab
    public float fireballSpeed;
    public float fireballCooldown = 2f;
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
                ThrowFireball(direction);
                lastFireballTime = Time.time;
            }
        }
    }

    void ThrowFireball(Vector2 direction)
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * fireballSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}