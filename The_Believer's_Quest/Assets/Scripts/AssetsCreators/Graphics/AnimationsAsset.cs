using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewAnimationsAsset", menuName = "Entity/Animations")]
public class AnimationsAsset : ScriptableObject
{
    [SerializeField] private AnimationClip[] move;
    [SerializeField] private AnimationClip[] attack;
    [SerializeField] private AnimationClip death;

    public AnimationClip[] Move { get => move; set => move = value; }
    public AnimationClip[] Attack { get => attack; set => attack = value; }
    public AnimationClip Death { get => death; set => death = value; }
}
