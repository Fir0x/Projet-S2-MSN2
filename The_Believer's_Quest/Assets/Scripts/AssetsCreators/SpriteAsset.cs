using UnityEngine;

[CreateAssetMenu(fileName = "NewSpriteAsset", menuName = "Sprite/Floor")]
public class SpriteAsset : ScriptableObject
{
    [SerializeField] private Sprite[] wall;
    [SerializeField] private Sprite[] floor;

    public Sprite[] Wall { get => wall; set => wall = value; }
    public Sprite[] Floor { get => floor; set => floor = value; }
}
