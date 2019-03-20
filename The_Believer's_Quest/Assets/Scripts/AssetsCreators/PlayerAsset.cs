using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerAsset", menuName = "Entity/Player")]
public class PlayerAsset : ScriptableObject
{
    [SerializeField] private int hp;
    [SerializeField] private int floor;
    [SerializeField] private int maxHP;
    [SerializeField] private int effectValue;
    [SerializeField] private int maxEffectValue;
    [SerializeField] private int gold;
    [SerializeField] private int diamond;
    [SerializeField] private Weapons[] weaponsList = new Weapons[2];
    [SerializeField] private int ammo;
    [SerializeField] private Weapons inHand;
    [SerializeField] private Vector2 position;

    public int Hp { get => hp; set => hp = value; }
    public int Floor { get => floor; set => floor = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int EffectValue { get => effectValue; set => effectValue = value; }
    public int MaxEffectValue { get => maxEffectValue; set => maxEffectValue = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Diamond { get => diamond; set => diamond = value; }
    public Weapons[] WeaponsList { get => weaponsList; set => weaponsList = value; }
    public Weapons InHand { get => inHand; set => inHand = value; }
    public Vector2 Position { get => position; set => position = value; }
    public int Ammo { get => ammo; set => ammo = value; }
}
