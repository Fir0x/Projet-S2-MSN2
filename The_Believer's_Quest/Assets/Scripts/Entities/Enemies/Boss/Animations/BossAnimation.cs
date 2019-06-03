using UnityEngine;
//Nicolas I
public class BossAnimation : MonoBehaviour
{
    [SerializeField] private BossAnimAsset anims;

    //public AnimationClip[] animationClips;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    public BossAnimAsset Anims { get => anims; set => anims = value; }

    public void Start()
    {
        string test = gameObject.name;
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animatorOverrideController["Move"] = anims.Move[0];
        animatorOverrideController["Attack"] = anims.Attack[0];
        animatorOverrideController["Appearance"] = anims.Appearance;
        animatorOverrideController["Change_phase"] = anims.Change_phase;
        animatorOverrideController["Death"] = anims.Death;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ChangeDirection(int direction)
    {
        animatorOverrideController["Move"] = anims.Move[direction + 4 * (animator.GetBool("phase2") ? 1 : 0)];
        animatorOverrideController["Attack"] = anims.Attack[direction + 4 * (animator.GetBool("phase2") ? 1 : 0)];
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Death()
    {
        animator.SetTrigger("death");
    }
}