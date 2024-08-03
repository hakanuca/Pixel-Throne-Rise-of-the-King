using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] private GameObject King; // Serialized King variable
    [SerializeField] private float bounce = 5f; // Bounce force

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == King) // Check if the collided object is the King
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) // Check if Rigidbody2D component exists
            {
                rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
        }
    }
}
