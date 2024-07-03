using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 nextPosition;
    public float damage = 1.3f;

    protected override void Start()
    {
        base.Start();
        if (pointA != null)
        {
            nextPosition = pointA.position;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        {
            nextPosition = nextPosition == pointA.position ? pointB.position : pointA.position;
        }
        MoveTowardsNextPosition();
    }

    void MoveTowardsNextPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assuming there's a Health component on the player that has a TakeDamage method.
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}