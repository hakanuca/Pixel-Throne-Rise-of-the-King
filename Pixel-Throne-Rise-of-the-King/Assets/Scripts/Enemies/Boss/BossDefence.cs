using UnityEngine;
using UnityEngine.Animations;

public class BossDefence : StateMachineBehaviour
{
    private BossHealth bossHealth;
    private bool isInvulnerable = false;
    private BossDefenceHelper helper;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHealth = animator.GetComponent<BossHealth>();
        helper = animator.GetComponent<BossDefenceHelper>();
        if (helper == null)
        {
            helper = animator.gameObject.AddComponent<BossDefenceHelper>();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        if (bossHealth != null && bossHealth.currentHealth <= 2 && !isInvulnerable)
        {
            isInvulnerable = true;
            bossHealth.SetInvulnerable(true);
            animator.SetBool("Defense", true);
            
            helper.StartDelay(5f, () => 
            {
                bossHealth.SetInvulnerable(false);
                isInvulnerable = false;
                animator.SetBool("Defense", false);
                animator.SetTrigger("ExitDefense");
            });
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Defense", false);
    }
}