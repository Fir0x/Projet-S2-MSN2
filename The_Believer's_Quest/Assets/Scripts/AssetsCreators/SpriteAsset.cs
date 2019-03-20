using UnityEngine;

[CreateAssetMenu(fileName = "NewSpriteAsset", menuName = "Sprite/Floor")]
public class SpriteAsset : ScriptableObject
{
    [SerializeField] private Sprite[] wall;
    [SerializeField] private Sprite[] floor;

    protected Sprite[] Wall { get => wall; set => wall = value; }
    protected Sprite[] Floor { get => floor; set => floor = value; }
}
