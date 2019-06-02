using UnityEngine;

//Nicolas I
[CreateAssetMenu(fileName = "NewBossAnimAsset", menuName = "AnimationStocker/Boss animations")]
public class BossAnimAsset : ScriptableObject
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip appearance;
    [SerializeField] private AnimationClip change_phase;
    [SerializeField] private AnimationClip death;

    public AnimationClip[] Move { get => move; set => move = value; }
    public AnimationClip[] Attack { get => attack; set => attack = value; }
    public AnimationClip Appearance { get => appearance; set => appearance = value; }
    public AnimationClip Change_phase { get => change_phase; set => change_phase = value; }
    public AnimationClip Death { get => death; set => death = value; }
}
