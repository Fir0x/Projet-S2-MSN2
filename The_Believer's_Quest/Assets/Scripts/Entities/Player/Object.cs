using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
public class Object : MonoBehaviour
{
    [SerializeField] private ObjectsAsset objectsAsset;

    public ObjectsAsset ObjectsAsset { get => objectsAsset; set => objectsAsset = value; }

    PlayerAsset playerAsset;

    private void Start()
    {
        playerAsset = GameObject.Find("Player").GetComponent<Player>().PlayerAsset;
    }

    public void PassiveChange()
    {
        playerAsset.MaxHP += objectsAsset.MaxHP;
        playerAsset.Hp += objectsAsset.HP;
        if (playerAsset.Hp > playerAsset.MaxHP)
            playerAsset.Hp = playerAsset.MaxHP;

        playerAsset.EffectValue += objectsAsset.EffectValue;
        playerAsset.MaxEffectValue += objectsAsset.MaxEffectValue;

        if (objectsAsset.Speed != 0)
            playerAsset.Speed *= objectsAsset.Speed;

    }

    public void ActiveChange()
    {

    }
}
