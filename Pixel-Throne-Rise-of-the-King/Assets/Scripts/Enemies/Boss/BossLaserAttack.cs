using System.Collections;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    public GameObject laserPrefab;
    public float timeBetweenAttacks = 3f;
    public float attackDuration = 1f;
    public float attackRange = 5f;

    private Transform player;
    private bool isAttacking;
    private float timeSinceLastAttack;
    private Boss boss;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GetComponent<Boss>();
    }

    void Update()
    {
        if (isAttacking)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackDuration)
            {
                isAttacking = false;
                timeSinceLastAttack = 0f;
            }
        }
        else if (Vector2.Distance(player.position, transform.position) <= attackRange)
        {
            boss.LookAtPlayer();
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(timeBetweenAttacks);

        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
