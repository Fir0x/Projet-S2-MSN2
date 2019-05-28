using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewEnemyAsset", menuName = "Entity/Enemy")]
public class EnemyAsset : ScriptableObject
{
    [SerializeField] private int hp = 10;
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 3;
    [SerializeField] [Range(2, 14)] private int minLoot;
    [SerializeField] [Range(3, 15)] private int maxLoot;
    [SerializeField] private int weight;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int nbofprojectiles;
    [SerializeField] private Attack.Trajectory trajectory;
    

    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    public int Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Weight { get => weight; set => weight = value; }
    public Sprite Sprite{get => sprite;set => sprite = value;}
    public int NbOfProjectiles{get => nbofprojectiles;set => nbofprojectiles = value;}
    public Attack.Trajectory _Trajectory{get => trajectory;set => trajectory = value;}
    
}

