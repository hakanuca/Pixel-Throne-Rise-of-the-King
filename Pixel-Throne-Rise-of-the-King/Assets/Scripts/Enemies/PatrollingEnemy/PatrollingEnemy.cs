using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 nextPosition;
    public float damage = 1.3f;

    private bool movingToPointB = true; // Track the current direction

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
            // Flip the direction
            if (movingToPointB)
            {
                nextPosition = pointA.position;
                Flip();
                movingToPointB = false;
            }
            else
            {
                nextPosition = pointB.position;
                Flip();
                movingToPointB = true;
            }
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

    // Method to flip the enemy's scale
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the x scale to make it face the other direction
        transform.localScale = theScale;
        // for character scale x to -x position
    }
}
