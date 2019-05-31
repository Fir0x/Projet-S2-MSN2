using UnityEngine;
//Nicolas I
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip death;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    private int animMoveID = Animator.StringToHash("direction");

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        /*animatorOverrideController["Move_Up"] = animationClips[0];
        animatorOverrideController["Move_Right"] = animationClips[1];
        animatorOverrideController["Move_Low"] = animationClips[2];
        animatorOverrideController["Move_Left"] = animationClips[3];
        animatorOverrideController["Attack"] = animationClips[4];
        animatorOverrideController["Death"] = animationClips[5];*/

        animatorOverrideController["Move"] = move[0];
        animatorOverrideController["Attack"] = attack[0];
        animatorOverrideController["Death"] = death;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ChangeMove()
    {
        animatorOverrideController["Move"] = move[animator.GetInteger(animMoveID)];
    }

    public void ChangeAttack()
    {
        animatorOverrideController["Attack"] = attack[animator.GetInteger(animMoveID)];
    }
}
