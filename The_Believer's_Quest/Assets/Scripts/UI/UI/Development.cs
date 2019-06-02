using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Nicolas I
public class Development : MonoBehaviour
{
    [SerializeField] protected PlayerAsset playerData;

    public void ChangeHp(Slider slider)
    {
        playerData.Hp = (int)slider.value;
    }

    public void ChangeMaxHp(Slider slider)
    {
        playerData.MaxHP = (int)slider.value;
    }

    public void AdaptHpGauge(Slider slider)
    {
        slider.maxValue = playerData.MaxHP;
    }

    public void ChangeEffectVal(Slider slider)
    {
        playerData.EffectValue = (int)slider.value;
    }

    public void ChangeMaxEffect(Slider slider)
    {
        playerData.MaxEffectValue = (int)slider.value;
    }

    public void AdaptEffectGauge(Slider slider)
    {
        slider.maxValue = playerData.MaxEffectValue;
    }

    public void ChangeGold(Slider slider)
    {
        playerData.Gold = (int)slider.value;
    }

    public void ChangeDiamond(Slider slider)
    {
        playerData.Diamond = (int)slider.value;
    }

    public void ChangeAmmo(Slider slider)
    {
        playerData.WeaponsList[0].GetComponent<Weapon>().GetAsset().Ammunitions = (int)slider.value;
    }

    public void Save()
    {
        Saver.SavePlayerData(playerData, new List<GameObject>());
    }

    public void ResetUnlocked()
    {
        List<GameObject> unlocked = Player.instance.UnlockedItems.Unlocked;
        List<GameObject> originLocked = unlocked.FindAll(item => unlocked.IndexOf(item) > 2);
        foreach (GameObject toLock in originLocked)
        {
            Player.instance.UnlockedItems.Locked.Add(toLock);
            Player.instance.UnlockedItems.Unlocked.Remove(toLock);
        }
    }
}
