using UnityEngine;
using UnityEngine.UI;

public class Development : MonoBehaviour
{
    [SerializeField] protected PlayerAsset player;

    public void ChangeHp(Slider slider)
    {
        player.Hp = (int)slider.value;
    }

    public void ChangeMaxHp(Slider slider)
    {
        player.MaxHP = (int)slider.value;
    }

    public void AdaptHpGauge(Slider slider)
    {
        slider.maxValue = player.MaxHP;
    }

    public void ChangeEffectVal(Slider slider)
    {
        player.EffectValue = (int)slider.value;
    }

    public void ChangeMaxEffect(Slider slider)
    {
        player.MaxEffectValue = (int)slider.value;
    }

    public void AdaptEffectGauge(Slider slider)
    {
        slider.maxValue = player.MaxEffectValue;
    }

    public void ChangeGold(Slider slider)
    {
        player.Gold = (int)slider.value;
    }

    public void ChangeDiamond(Slider slider)
    {
        player.Diamond = (int)slider.value;
    }

    public void ChangeAmmo(Slider slider)
    {
        player.InHand.Ammunitions = (int)slider.value;
    }
}
