﻿using System.Collections;
using UnityEngine;
//Nicolas I
public class ObjectEffects : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;

    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }

    public void Apply(ObjectsAsset item)
    {
        playerAsset.Hp += item.HP;
        playerAsset.MaxHP += item.MaxHP;
        playerAsset.EffectValue += item.EffectValue;
        playerAsset.MaxEffectValue += item.MaxEffectValue;
        playerAsset.Speed += item.Speed;

        if (item.Duration != 0)
        {
            StartCoroutine(Duration(item.Duration));
            playerAsset.Hp -= item.HP;
            playerAsset.MaxHP -= item.MaxHP;
            playerAsset.EffectValue -= item.EffectValue;
            playerAsset.MaxEffectValue -= item.MaxEffectValue;
            playerAsset.Speed -= item.Speed;
        }
    }

    IEnumerator Duration(uint duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
