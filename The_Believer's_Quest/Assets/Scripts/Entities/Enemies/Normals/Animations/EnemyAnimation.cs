using UnityEngine;
//Nicolas I
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private AnimationsAsset anims;

    //public AnimationClip[] animationClips;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    public AnimationsAsset Anims { get => anims; set => anims = value; }

    public void Start()
    {
        string test = gameObject.name;
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        /*animatorOverrideController["Move_Up"] = anims.Move[0];
        animatorOverrideController["Move_Right"] = anims.Move[1];
        animatorOverrideController["Move_Down"] = anims.Move[2];
        animatorOverrideController["Move_Left"] = anims.Move[3];
        animatorOverrideController["Attack_Up"] = anims.Attack[1];
        animatorOverrideController["Attack_Right"] = anims.Attack[2];
        animatorOverrideController["Attack_Down"] = anims.Attack[3];
        animatorOverrideController["Attack_Left"] = anims.Attack[4];
        animatorOverrideController["Death"] = anims.Death;*/
        
        animatorOverrideController["Move_Up"] = anims.Move[0];
        animatorOverrideController["Attack"] = anims.Attack[0];
        animatorOverrideController["Death"] = anims.Death;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ChangeMove()
    {
        print("direction: " + animator.GetInteger("direction"));
        animatorOverrideController["Move"] = anims.Move[animator.GetInteger("direction")];
    }

    public void ChangeAttack()
    {
        animatorOverrideController["Attack"] = anims.Attack[animator.GetInteger("direction")];
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
    }
}
