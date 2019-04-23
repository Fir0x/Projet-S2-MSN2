using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        hp.value = player.Hp;
        maxHp.value = player.MaxHP;
        effect.value = player.EffectValue;
        maxEffect.value = player.MaxEffectValue;
        Weapon w = gameObject.GetComponentInChildren<Weapon>();
        weaponSprite.sprite = w.GetAsset().Sprite;
        gold.text = player.Gold +"";
        diamond.text = player.Diamond +"";
        Ammo.text = player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions;
    }
}
