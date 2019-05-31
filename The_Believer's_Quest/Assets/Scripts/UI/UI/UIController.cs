using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Sarah (ancienne version) / Nicolas I (version actuelle)
public class UIController : MonoBehaviour
{
    public Text gold;
    public Text diamond;
    public Text ammo;
    public Image weaponSprite;
    
    [SerializeField] public PlayerAsset player;

    [SerializeField] private Slider hp;
    [SerializeField] private Slider effect;

    public UnityEvent changeHp;
    public UnityEvent changeMaxHp;
    public UnityEvent changeEffect;
    public UnityEvent changeMaxEffect;
    public UnityEvent changeGold;
    public UnityEvent changeDiamond;
    public UnityEvent changeWeapon;
    public UnityEvent changeAmmo;

    public static UIController uIController; //Singleton

    public Slider Hp { get => hp; set => hp = value; }
    public Slider Effect { get => effect; set => effect = value; }

    private void Start()
    {
        //Init UI parts with good values
        hp.maxValue = player.MaxHP;
        hp.value = player.Hp;
        effect.maxValue = player.MaxEffectValue;
        effect.value = player.EffectValue;
        gold.text = player.Gold + "";
        diamond.text = player.Diamond + "";
        weaponSprite.sprite = player.WeaponsList[0].Sprite;
        ammo.text = player.WeaponsList[0].Type == WeaponAsset.WeaponType.CQC ?
                                         "" : player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions;

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

        changeGold.AddListener(() => gold.text = player.Gold + "");

        changeDiamond.AddListener(() => diamond.text = player.Diamond + "");

        changeWeapon.AddListener(() => weaponSprite.sprite = player.WeaponsList[0].Sprite);

        changeAmmo.AddListener(() => ammo.text = player.WeaponsList[0].Type == WeaponAsset.WeaponType.CQC ? 
                                         "" : player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions);

        uIController = this;
    }
}
