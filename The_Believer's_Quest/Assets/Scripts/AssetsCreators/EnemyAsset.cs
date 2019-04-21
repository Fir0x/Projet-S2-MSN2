using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyAsset", menuName = "Entity/Enemy")]
public class EnemyAsset : ScriptableObject
{
    [SerializeField] private int hp = 10;
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 3;
    [SerializeField] [Range(2, 14)] private int minLoot;
    [SerializeField] [Range(3, 15)] private int maxLoot;

    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    protected int Hp { get => hp; set => hp = value; }
    protected float Speed { get => speed; set => speed = value; }
    protected int Damage { get => damage; set => damage = value; }
}
