using UnityEngine;
using UnityEngine.Animations;

public class BossDefence : StateMachineBehaviour
{
    private BossHealth bossHealth;
    private bool isInvulnerable = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHealth = animator.GetComponent<BossHealth>();
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 2 && !isInvulnerable)
        {
            isInvulnerable = true;
            bossHealth.TakeDamage(0);
            Debug.Log("Setting Defense to true");
            animator.SetBool("Defense", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Defense");
    }
}