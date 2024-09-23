using System.Collections;
using UnityEngine;

public class StarBase : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float cooldown = 3f;
    private Boss boss;
    private Transform player;
    [SerializeField] private LayerMask playerLayer;
    private float lastAttackTime;
    [SerializeField] private float attackSpeed = 20f;

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

        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position + direction * 5f;

        StartCoroutine(MoveToPosition(targetPosition, attackSpeed));

        lastAttackTime = Time.time;
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(target.x, startPosition.y, startPosition.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(3f);
        
        Attack();
    }
    
}