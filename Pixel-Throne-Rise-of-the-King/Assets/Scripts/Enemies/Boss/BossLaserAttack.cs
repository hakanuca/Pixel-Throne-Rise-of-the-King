using UnityEngine;

public class BossLaserAttack : StateMachineBehaviour
{
    public GameObject laserPrefab;
    private Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.boss.LookAtPlayer();
        Transform bossTransform = animator.transform;
        Boss boss = bossTransform.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Laser");
    }

    
}