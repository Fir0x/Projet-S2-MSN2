using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimationsAsset", menuName = "Entity/Enemy Animations")]
public class BossAnimAsset : MonoBehaviour
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip change_phase;
    [SerializeField] private AnimationClip death;

    public AnimationClip[] Move { get => move; set => move = value; }
    public AnimationClip[] Attack { get => attack; set => attack = value; }
    public AnimationClip Change_phase { get => change_phase; set => change_phase = value; }
    public AnimationClip Death { get => death; set => death = value; }
}
