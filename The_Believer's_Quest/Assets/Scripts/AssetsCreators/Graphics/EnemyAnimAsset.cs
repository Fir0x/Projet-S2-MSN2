using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewEnemyAnimsAsset", menuName = "AnimationStocker/Enemy animations")]
public class EnemyAnimAsset : ScriptableObject
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip death;

    public AnimationClip[] Move { get => move; set => move = value; }
    public AnimationClip[] Attack { get => attack; set => attack = value; }
    public AnimationClip Death { get => death; set => death = value; }
}
