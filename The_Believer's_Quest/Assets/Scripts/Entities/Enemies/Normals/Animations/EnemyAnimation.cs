using UnityEngine;
//Nicolas I
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyAnimAsset anims;

    //public AnimationClip[] animationClips;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    public EnemyAnimAsset Anims { get => anims; set => anims = value; }

    public void Start()
    {
        string test = gameObject.name;
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        
        animatorOverrideController["Move"] = anims.Move[0];
        animatorOverrideController["Attack"] = anims.Attack[0];
        animatorOverrideController["Death"] = anims.Death;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ChangeDirection(int direction)
    {
        animatorOverrideController["Move"] = anims.Move[direction];
        animatorOverrideController["Attack"] = anims.Attack[direction];
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
