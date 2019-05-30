using UnityEngine;

[CreateAssetMenu(fileName = "NewBoss", menuName = "Entity/Boss")]
public class BossAsset : ScriptableObject
{
    [SerializeField] private int hp = 200;
    [SerializeField] private int maxHp;
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 10;
    [SerializeField] [Range(2, 14)] private int minLoot;
    [SerializeField] [Range(3, 15)] private int maxLoot;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float range;


    public int Hp { get => hp; set => hp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MinLoot { get => minLoot; set => minLoot = value; }
    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public float Range { get => range; set => range = value; }

    private void Awake()
    {
        maxHp = hp;
    }
}