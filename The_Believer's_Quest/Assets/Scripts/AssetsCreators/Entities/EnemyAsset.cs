using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewEnemyAsset", menuName = "Entity/Enemy")]
public class EnemyAsset : ScriptableObject
{
    [SerializeField] private float hp = 10;
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 3;
    [SerializeField] [Range(2, 14)] private int minLoot;
    [SerializeField] [Range(3, 15)] private int maxLoot;
    [SerializeField] private int weight;
    [SerializeField] private int cooldown;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int nbofprojectiles;
    [SerializeField] private Attack.Trajectory trajectory;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float range;
    [SerializeField] private Sprite projectile;
    
    
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MinLoot { get => minLoot; set => minLoot = value; }
    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    public int Weight { get => weight; set => weight = value; }
    public int Cooldown { get => cooldown; set => cooldown = value; }
    public Sprite Sprite{get => sprite;set => sprite = value;}
    public int NbOfProjectiles{get => nbofprojectiles;set => nbofprojectiles = value;}
    public Attack.Trajectory _Trajectory{get => trajectory;set => trajectory = value;}
    public float ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
    public float Range { get => range; set => range = value; }

    public Sprite Projectile { get => projectile; set => projectile = value; }
}

