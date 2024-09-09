using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public float patrolDistance = 5f; 
    public float speed = 2f;          
    private Vector3 originPosition;   
    private Vector3 nextPosition;      
    private bool movingRight = true;   

    protected override void Start()
    {
        base.Start();
        originPosition = transform.position;  
        nextPosition = originPosition + Vector3.right * patrolDistance;  
    }

    void Update()
    {
       
        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        {
           
            if (movingRight)
            {
                nextPosition = originPosition + Vector3.left * patrolDistance;
                Flip();
                movingRight = false;
            }
            else
            {
                nextPosition = originPosition + Vector3.right * patrolDistance;
                Flip();
                movingRight = true;
            }
        }

        MoveTowardsNextPosition();
    }

    void MoveTowardsNextPosition()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        
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
