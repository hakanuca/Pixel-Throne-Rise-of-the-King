using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public Animator animator;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public GameObject laserPrefab;
    public float minDistance = 20f;
    public float cooldown = 3f; 
    float lastAttackTime;
    Boss boss;
    Transform player;
    public LayerMask playerLayer;

    private void Start()
    {
        boss = GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -cooldown;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= minDistance)
        {
            
        }
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
    }

    #region Gizmos

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
    
    void OnDrawGizmos()
    {
        if (boss == null) return;

        Transform bossTransform = boss.transform;
        Transform playerTransform = boss.player;

        Vector3 direction = (playerTransform.position - bossTransform.position).normalized;
        Vector3 rayOrigin = bossTransform.position + new Vector3(0, 1, 0); // Adjust the y-axis
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOrigin, direction * minDistance);
    }

    #endregion
}