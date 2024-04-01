using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startPos = pointA.position;
        endPos = pointB.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);

        if (fracJourney >= 1.0f)
        {
            // Swap start and end points
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;

            // Reset start time
            startTime = Time.time;
        }
    }
}