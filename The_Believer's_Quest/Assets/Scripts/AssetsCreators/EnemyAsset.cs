using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyAsset", menuName = "Entity/Enemy")]
public class EnemyAsset : ScriptableObject
{
    [SerializeField] private int hp = 2;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] [Range(2, 15)] private int maxLoot;

    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    protected int Hp { get => hp; set => hp = value; }
    protected float Speed { get => speed; set => speed = value; }
    protected int Damage { get => damage; set => damage = value; }
}
