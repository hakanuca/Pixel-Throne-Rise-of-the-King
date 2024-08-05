using System.Collections;
using UnityEngine;

public class StarBase : MonoBehaviour
{
    public Animator animator;
    public float cooldown = 3f;
    private Boss boss;
    private Transform player;
    public LayerMask playerLayer;
    private float lastAttackTime;

    private void Start()
    {
        boss = GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -cooldown;
    }

    private void Update()
    {
        if (Time.time - lastAttackTime >= cooldown)
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        float attackSpeed = 20f;
        StartCoroutine(MoveToPosition(targetPosition, attackSpeed));
        
        lastAttackTime = Time.time;
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}