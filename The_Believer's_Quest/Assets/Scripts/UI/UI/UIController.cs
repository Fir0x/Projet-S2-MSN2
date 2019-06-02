using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Sarah (ancienne version) / Nicolas I (version actuelle)
public class UIController : MonoBehaviour
{
    [SerializeField] private Text gold;
    [SerializeField] private Text diamond;
    [SerializeField] private Text ammo;
    [SerializeField] private Image weaponSprite;

    [SerializeField] public PlayerAsset player;

    [SerializeField] private Slider hp;
    [SerializeField] private Slider effect;
    [SerializeField] private GameObject mapPannel;

    public UnityEvent changeHp;
    public UnityEvent changeMaxHp;
    public UnityEvent changeEffect;
    public UnityEvent changeMaxEffect;
    public UnityEvent changeGold;
    public UnityEvent changeDiamond;
    public UnityEvent changeWeapon;
    public UnityEvent changeAmmo;

    public static UIController uIController; //Singleton

    private float originalTimeScale;

    public Text Gold { get => gold; set => gold = value; }
    public Text Diamond { get => diamond; set => diamond = value; }
    public Text Ammo { get => ammo; set => ammo = value; }
    public Image WeaponSprite { get => weaponSprite; set => weaponSprite = value; }
    public Slider Hp { get => hp; set => hp = value; }
    public Slider Effect { get => effect; set => effect = value; }
    public GameObject Map { get => mapPannel; set => mapPannel = value; }

    private void Start()
    {
        uIController = this; //Initi singleton

        originalTimeScale = Time.timeScale;

        //Init UI parts with good values
        hp.maxValue = player.MaxHP;
        hp.value = player.Hp;
        effect.maxValue = player.MaxEffectValue;
        effect.value = player.EffectValue;
        Gold.text = player.Gold + "";
        Diamond.text = player.Diamond + "";
        WeaponSprite.sprite = player.WeaponsList[0].GetAsset().Sprite;
        Ammo.text = player.WeaponsList[0].Type == WeaponAsset.WeaponType.CQC ?
                                         "" : player.WeaponsList[0].GetAsset().Loader + " / " + player.WeaponsList[0].GetAsset().Ammunitions;

        //Init all events to change UI parts
        if (changeHp == null)
            changeHp = new UnityEvent();

        if (changeMaxHp == null)
            changeMaxHp = new UnityEvent();

        if (changeEffect == null)
            changeEffect = new UnityEvent();

        if (changeMaxEffect == null)
            changeMaxEffect = new UnityEvent();

        if (changeGold == null)
            changeGold = new UnityEvent();

        if (changeDiamond == null)
            changeDiamond = new UnityEvent();

        if (changeWeapon == null)
            changeWeapon = new UnityEvent();

        if (changeAmmo == null)
            changeAmmo = new UnityEvent();

        changeHp.AddListener(() => hp.value = player.Hp);

        changeMaxHp.AddListener(() => hp.maxValue = player.MaxHP);

        changeEffect.AddListener(() => effect.value = player.EffectValue);

        changeMaxEffect.AddListener(() => effect.maxValue = player.MaxEffectValue);

        changeGold.AddListener(() => Gold.text = player.Gold + "");

        changeDiamond.AddListener(() => Diamond.text = player.Diamond + "");

        changeWeapon.AddListener(() => WeaponSprite.sprite = player.WeaponsList[0].GetAsset().Sprite);

        changeAmmo.AddListener(() => Ammo.text = player.WeaponsList[0].Type == WeaponAsset.WeaponType.CQC ? 
                                         "" : player.WeaponsList[0].GetAsset().Loader + " / " + player.WeaponsList[0].GetAsset().Ammunitions);
    }

    public void ShowMap()
    {
        mapPannel.SetActive(!mapPannel.activeSelf);

        if (mapPannel.activeSelf)
        {
            foreach (Slider scrollbar in mapPannel.GetComponentsInChildren<Slider>())
                scrollbar.value = 0.5f;
        }
    }
}
