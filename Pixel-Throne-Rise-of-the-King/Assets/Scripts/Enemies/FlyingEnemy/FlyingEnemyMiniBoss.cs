using UnityEngine;
using System.Collections;

public class FlyingEnemyMiniBoss : Enemy
{
    public GameObject player;
    public float speed;
    public int damage;
    public float distanceBetween;
    private float distance;

    [SerializeField] private GameObject fireballPrefab; 
    public float fireballSpeed;
    public float fireballCooldown = 3f; 
    
    private float lastFireballTime;

    private Animator animator; 

    protected override void Start()
    {
        base.Start();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < distanceBetween)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Flip the enemy to face the player
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Time.time > lastFireballTime + fireballCooldown)
            {
                ThrowFireball();
                lastFireballTime = Time.time;
            }
        }
    }

    void ThrowFireball()
    {
        animator.SetTrigger("Attack"); // Trigger the attack animation
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        
        StartCoroutine(FollowPlayer(fireball));
        Destroy(fireball, 3f); 
    }
    
    IEnumerator FollowPlayer(GameObject fireball)
    {
        while (fireball != null)
        {
            Vector2 direction = (player.transform.position - fireball.transform.position).normalized;
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            rb.velocity = direction * fireballSpeed;

            // Flip the fireball to face the player
            if (direction.x > 0)
            {
                fireball.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x < 0)
            {
                fireball.transform.localScale = new Vector3(1, 1, 1);
            }

            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Hurt");
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}