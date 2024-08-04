using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;  // Starting point
    [SerializeField] private Transform pointB;  // Ending point
    [SerializeField] private float speed = 1f;  // Movement speed

    private bool movingTowardsB = true; // Track movement direction
    private Vector3 originalScale; // Store the original scale

    private void Start()
    {
        // Store the original scale at the start
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Calculate the position based on time
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);

        // Determine if we are at pointA or pointB and adjust scale
        if (t >= 0.99f && movingTowardsB)
        {
            // Reached pointB, flip the X scale
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            movingTowardsB = false;
        }
        else if (t <= 0.01f && !movingTowardsB)
        {
            // Reached pointA, flip the X scale back
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            movingTowardsB = true;
        }
    }
}
