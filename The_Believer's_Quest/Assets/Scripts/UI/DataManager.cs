using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    [SerializeField] protected PlayerAsset player;
    public Text gold;
    public Text diamond;
    public Text Ammo;
    
    public void ChangeHp(Slider slider)
    {
        slider.value = player.Hp;
    }
    public void ChangeMaxHp(Slider slider)
    {
        slider.maxValue = player.MaxHP;
    }
    public void ChangeEffectVal(Slider slider)
    {
        slider.value = player.EffectValue;
    }
    public void ChangeMaxEffect(Slider slider)
    {
        slider.maxValue = player.MaxEffectValue;
    }

    void Update()
    {
        gold.text = player.Gold + " Ors";
        diamond.text = player.Diamond + " Diamants";
        Ammo.text = player.WeaponsList[0].Loader + " / " + player.WeaponsList[0].Ammunitions;
    }
}
