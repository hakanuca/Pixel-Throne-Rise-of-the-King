using UnityEngine;

public class FlyingEnemy : Enemy
{
    public GameObject player;
    public float speed;
    public int damage;
    public float distanceBetween;
    private float distance;

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
        }
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