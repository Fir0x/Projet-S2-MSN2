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

    [SerializeField] public Slider hp;
    [SerializeField] public Slider maxHp;
    [SerializeField] public Slider effect;
    [SerializeField] public Slider maxEffect;

    public UnityEvent changeHp;
    public UnityEvent changeMaxHp;
    public UnityEvent changeEffect;
    public UnityEvent changeMaxEffect;
    public UnityEvent changeGold;
    public UnityEvent changeDiamond;
    public UnityEvent changeWeapon;
    public UnityEvent changeAmmo;

    private void Awake()
    {
        if (changeHp == null)
        {
            changeHp = new UnityEvent();
            changeHp.AddListener(() => hp.value = player.Hp);
        }

        if (changeMaxHp == null)
        {
            changeMaxHp = new UnityEvent();
            changeMaxHp.AddListener(() => hp.maxValue = player.MaxHP);
        }

        if (changeEffect == null)
        {
            changeEffect = new UnityEvent();
            changeEffect.AddListener(() => effect.value = player.EffectValue);
        }

        if (changeMaxEffect == null)
        {
            changeMaxEffect = new UnityEvent();
            changeMaxEffect.AddListener(() => effect.maxValue = player.MaxEffectValue);
        }

        if (changeGold == null)
        {
            changeGold = new UnityEvent();
            changeGold.AddListener(() => gold.text = player.Gold + "");
        }

        if (changeDiamond == null)
        {
            changeDiamond = new UnityEvent();
            changeDiamond.AddListener(() => diamond.text = player.Diamond + "");
        }

        if (changeWeapon == null)
        {
            changeWeapon = new UnityEvent();
            changeWeapon.AddListener(() => weaponSprite.sprite = player.WeaponsList[0].Sprite);
        }

        if (changeAmmo == null)
        {
            changeAmmo = new UnityEvent();
            changeAmmo.AddListener(() => ammo.text = player.WeaponsList[0].Type == WeaponAsset.WeaponType.CQC ? 
                                         "" : player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions);
        }
    }
}
