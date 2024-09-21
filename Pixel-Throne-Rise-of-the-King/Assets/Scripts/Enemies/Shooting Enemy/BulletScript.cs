using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            float rotation = Mathf.Atan2(-direction.y, -direction.x);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

