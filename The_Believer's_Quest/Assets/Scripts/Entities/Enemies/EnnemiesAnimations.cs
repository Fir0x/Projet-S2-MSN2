using UnityEngine;

public class EnnemiesAnimations : MonoBehaviour
{
    [SerializeField] protected AnimationClip[] animationClips;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    public void Start()
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
    }
}
