using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    // This is the speed value of the elevator system. // Should be tested !!!!!
    public float speed = 2f;
    public Transform topPoint;
    public Transform bottomPoint;
    public bool movingUp = true;

    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, topPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, topPoint.position) < 0.1f)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, bottomPoint.position) < 0.1f)
            {
                movingUp = true;
            }
        }
    }
}
