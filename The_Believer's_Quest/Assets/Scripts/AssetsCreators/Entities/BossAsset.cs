using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewBoss", menuName = "Entity/Boss")]
public class BossAsset : ScriptableObject
{
    [SerializeField] private float hp = 200;
    [SerializeField] private float maxHp;
    [SerializeField] private float speed = 1;
    [SerializeField] private float damage = 10;
    [SerializeField] [Range(2, 14)] private int minLoot;
    [SerializeField] [Range(3, 15)] private int maxLoot;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;
    [SerializeField] private bool effect;


    public float Hp { get => hp; set => hp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Damage { get => damage; set => damage = value; }
    public int MinLoot { get => minLoot; set => minLoot = value; }
    public int MaxLoot { get => maxLoot; set => maxLoot = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public float Range { get => range; set => range = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public bool Effect { get => effect; set => effect = value; }

    private void Awake()
    {
        maxHp = hp;
    }
}