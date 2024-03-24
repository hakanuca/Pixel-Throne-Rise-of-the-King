using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnPlayer : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;

    private Rigidbody2D rb;

    private Coroutine knockbackCoroutine;

    public bool IsBeingKnockedBack {get; private set;}

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockBackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < knockbackTime)
        {
            //iterate the timer
            _elapsedTime += Time.fixedDeltaTime;

            //combine _hitForce and _constantForce
            _knockBackForce = _hitForce + _constantForce;

            //combine knockBackForce with Input Force
            if (inputDirection != 0)
            {
                _combinedForce = _knockBackForce + new Vector2(inputDirection, 0f);
            }
            else
            {
                _combinedForce = _knockBackForce;
            }

            //apply knockback
            rb.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();
        }

        IsBeingKnockedBack = false;
    }

    public void CallKnocbkack(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
