using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "Items/Object")]
public class ObjectsAsset : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool passive;
    [SerializeField] private int price;
    [SerializeField] private int hP;
    [SerializeField] private int maxHP;
    [SerializeField] private int effectValue;
    [SerializeField] private int maxEffectValue;
    [SerializeField] private int speed;
    [SerializeField] private int ammo;
    [SerializeField] private bool invicibility;
    [SerializeField] private uint duration;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public bool Passive { get => passive; set => passive = value; }
    public int Price { get => price; set => price = value; }
    public int HP { get => hP; set => hP = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int EffectValue { get => effectValue; set => effectValue = value; }
    public int MaxEffectValue { get => maxEffectValue; set => maxEffectValue = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    public bool Invicibility { get => invicibility; set => invicibility = value; }
    public uint Duration { get => duration; set => duration = value; }
}
