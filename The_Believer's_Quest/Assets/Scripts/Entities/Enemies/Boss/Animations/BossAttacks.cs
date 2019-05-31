using UnityEngine;

public class BossAttacks : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.gameObject.GetComponent<EnemyAnimation>().ChangeAttack();
    }
}