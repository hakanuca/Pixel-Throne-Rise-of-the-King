using System.Collections;
using UnityEngine;

public class IsRolling : MonoBehaviour
{
    public float rollDuration = 1f;
    public float rollSpeed = 5f;
    public float rollCooldown = 1f;
    private float nextRollTime = 0f;
    public bool isRolling = false;
    private Animator animator; 

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
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

        float endTime = Time.time + rollDuration;
        Vector2 rollDirection = transform.localScale.x == 1 ? Vector2.right : Vector2.left;
        while (Time.time < endTime)
        {
            transform.Translate(rollDirection * rollSpeed * Time.deltaTime);
            yield return null;
        }

        gameObject.layer = playerLayer;
        isRolling = false;
    }
}