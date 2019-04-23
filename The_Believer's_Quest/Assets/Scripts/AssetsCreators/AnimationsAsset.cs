using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimationsAsset", menuName = "Entity/Animations!mm")]
public class AnimationsAsset : ScriptableObject
{
    [SerializeField] private Animation[] animations;

    public Animation[] Animations { get => animations; set => animations = value; }
}
