using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewPlayerAsset", menuName = "Entity/Player")]
[System.Serializable]
public class PlayerAsset : ScriptableObject
{
    [SerializeField] private int floor;
    [SerializeField] private int hp = 50;
    [SerializeField] private int maxHP = 50;
    [SerializeField] private int effectValue;
    [SerializeField] private int maxEffectValue = 30;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool invicibility;
    [SerializeField] private int gold;
    [SerializeField] private int diamond;
    [SerializeField] private WeaponAsset[] weaponsList = new WeaponAsset[2];
   
    public int Floor { get => floor; set => floor = value; }
    public int Hp { get => hp; set => hp = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int EffectValue { get => effectValue; set => effectValue = value; }
    public int MaxEffectValue { get => maxEffectValue; set => maxEffectValue = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool Invicibility { get => invicibility; set => invicibility = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Diamond { get => diamond; set => diamond = value; }
    public WeaponAsset[] WeaponsList { get => weaponsList; set => weaponsList = value; }

    public string SerializeWeapons()
    {
        return (weaponsList[0] != null ? weaponsList[0].name : "null") + "," + (weaponsList[1] != null ? weaponsList[1].name : "null");
    }
}
