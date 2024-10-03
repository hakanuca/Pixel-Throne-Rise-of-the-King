using System;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;
    private float timer;
    private GameObject player;
    [SerializeField] private float shootingDistance = 10f; 
    [SerializeField] private float cooldownTime = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = cooldownTime; 
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer += Time.deltaTime; 
        Debug.Log("Player is null");
        if (player != null)
        {
            Debug.Log("Player is not null");
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < shootingDistance && timer >= cooldownTime)
            {
                Shoot();
                timer = 0; 
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("Shooting");
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }

    // Draw the shooting range in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
    }
}