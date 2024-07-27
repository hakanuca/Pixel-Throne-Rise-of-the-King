using System.Collections;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public float rollDuration = 1f;
    public float rollSpeed = 5f;
    public float rollCooldown = 1f;
    private float nextRollTime = 0f;
    public bool isRolling = false;
    private Animator animator;
    private Collider2D[] colliders;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Time.time >= nextRollTime)
        {
            Roll();
        }
    }

    void Roll()
    {
        nextRollTime = Time.time + rollCooldown;
        animator.SetTrigger("Rolling");
        StartCoroutine(PerformRoll());
    }

    IEnumerator PerformRoll()
    {
        isRolling = true;
        int playerLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Rolling");

        // Disable all colliders
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        // Freeze Y-axis
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        float endTime = Time.time + rollDuration;
        Vector2 rollDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        while (Time.time < endTime)
        {
            transform.Translate(rollDirection * rollSpeed * Time.deltaTime);
            yield return null;
        }

        // Re-enable all colliders
        foreach (var col in colliders)
        {
            col.enabled = true;
        }

        // Unfreeze Y-axis
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        gameObject.layer = playerLayer;
        isRolling = false;
    }
}