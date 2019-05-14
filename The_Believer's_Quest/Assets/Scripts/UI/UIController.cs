using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Sarah
public class UIController : MonoBehaviour
{
    public Text gold;
    public Text diamond;
    public Text Ammo;
    public Image weaponSprite;
    
    [SerializeField] public PlayerAsset player;

    [SerializeField] public Slider hp;
    [SerializeField] public Slider maxHp;
    [SerializeField] public Slider effect;
    [SerializeField] public Slider maxEffect;


    void FixedUpdate()
    {
        hp.value = player.Hp;
        maxHp.value = player.MaxHP;
        effect.value = player.EffectValue;
        maxEffect.value = player.MaxEffectValue;
        weaponSprite.sprite = player.WeaponsList[0].Sprite;
        gold.text = player.Gold +"";
        diamond.text = player.Diamond +"";
        if (player.WeaponsList[0].Cqc)
            Ammo.text = "";
        else
            Ammo.text = player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions;
    }
}
