using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 nextPosition;
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

    // Method to flip the enemy's scale
    void Flip()
    {
        // Flip the enemy's scale
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        // Flip the scale of all child objects
        foreach (Transform child in transform)
        {
            Vector3 childScale = child.localScale;
            childScale.x *= -1;
            child.localScale = childScale;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}