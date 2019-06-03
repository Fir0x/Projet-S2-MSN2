using UnityEngine;
//Nicolas I
[CreateAssetMenu(fileName = "NewPlayerAsset", menuName = "Entity/Player")]
[System.Serializable]
public class PlayerAsset : ScriptableObject
{
    [SerializeField] private int floor;
    [SerializeField] private float hp = 50;
    [SerializeField] private float maxHP = 50;
    [SerializeField] private float effectValue;
    [SerializeField] private int maxEffectValue = 30;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool invicibility;
    [SerializeField] private int gold;
    [SerializeField] private int diamond;
    [SerializeField] private GameObject[] weaponsList = new GameObject[2];
    [SerializeField] private GameObject[] objectsList = new GameObject[2];
    [SerializeField] private Vector3 position;
   
    public int Floor { get => floor; set => floor = value; }
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp < 0)
                hp = 0;
        }
    }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public float EffectValue
    {
        get => effectValue;
        set
        {
            effectValue = value;
            if (effectValue < 0)
                effectValue = 0;
        }
    }
    public int MaxEffectValue { get => maxEffectValue; set => maxEffectValue = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool Invicibility { get => invicibility; set => invicibility = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Diamond { get => diamond; set => diamond = value; }
    public GameObject[] WeaponsList { get => weaponsList; set => weaponsList = value; }
    public Vector3 Position { get => position; set => position = value; }
    public GameObject[] ObjectsList { get => objectsList; set => objectsList = value; }
}
