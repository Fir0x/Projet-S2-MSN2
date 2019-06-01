using UnityEngine;
//Nicolas I
public class EnemyAnimation : MonoBehaviour
{
    /*[SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip death;*/

    public AnimationClip[] animationClips;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animatorOverrideController["Move_Up"] = animationClips[0];
        animatorOverrideController["Move_Right"] = animationClips[1];
        animatorOverrideController["Move_Low"] = animationClips[2];
        animatorOverrideController["Move_Left"] = animationClips[3];
        animatorOverrideController["Attack"] = animationClips[4];
        animatorOverrideController["Death"] = animationClips[5];
        animator.runtimeAnimatorController = animatorOverrideController;

        /*animatorOverrideController["Move"] = move[0];
        animatorOverrideController["Attack"] = attack[0];
        animatorOverrideController["Death"] = death;*/
    }

    /*public void ChangeMove()
    {
        print("direction: " + animator.GetInteger("direction"));
        animatorOverrideController["Move"] = move[animator.GetInteger("direction")];
    }

    public void ChangeAttack()
    {
        animatorOverrideController["Attack"] = attack[animator.GetInteger("direction")];
    }

    public void Direction(int direction)
    {
        animator.SetInteger("direction", direction);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Death()
    {
        animator.SetTrigger("death");
    }*/
}
