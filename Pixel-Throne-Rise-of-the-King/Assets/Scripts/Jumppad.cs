using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    private float bounce = 5f;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Main_Character")){
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce,ForceMode2D.Impulse);
        }
    }
}
