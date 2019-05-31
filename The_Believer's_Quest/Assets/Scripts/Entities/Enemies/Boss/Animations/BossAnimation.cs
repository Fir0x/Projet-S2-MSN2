using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip changePhase;
    [SerializeField] private AnimationClip death;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    private int animMoveID = Animator.StringToHash("direction");
    private int animPhaseTwoID = Animator.StringToHash("phase2");

    public AnimationClip[] Move { get => move; set => move = value; }
    public AnimationClip[] Attack { get => attack; set => attack = value; }
    public AnimationClip ChangePhase { get => changePhase; set => changePhase = value; }
    public AnimationClip Death { get => death; set => death = value; }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animatorOverrideController["Move"] = move[0];
        animatorOverrideController["Attack"] = attack[0];
        animatorOverrideController["ChangePhase"] = changePhase;
        animatorOverrideController["Death"] = death;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ChangeMove()
    {
        animatorOverrideController["Move"] = move[animator.GetInteger(animMoveID) + 4 * (animator.GetBool(animPhaseTwoID) ? 1 : 0)];
    }

    public void ChangeAttack()
    {
        animatorOverrideController["Attack"] = attack[animator.GetInteger(animMoveID) + 4 * (animator.GetBool(animPhaseTwoID) ? 1 : 0)];
    }
}
