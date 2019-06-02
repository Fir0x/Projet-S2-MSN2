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
    }

    public void PassiveChange()
    {
        playerAsset = GameObject.Find("Player").GetComponent<Player>().PlayerAsset;
        
        if(objectsAsset.HP != 0)
        {
            GameObject.Find("Player").GetComponent<Player>().SetLife(playerAsset.Hp + objectsAsset.HP);
        }

        if (objectsAsset.MaxHP != 0)
        {
            GameObject.Find("Player").GetComponent<Player>().SetMaxLife(playerAsset.MaxHP + objectsAsset.MaxHP);
        }
        
        if (objectsAsset.EffectValue != 0)
        {
            GameObject.Find("Player").GetComponent<Player>().SetEffect(playerAsset.EffectValue + objectsAsset.EffectValue);
        }

        if (objectsAsset.MaxEffectValue != 0)
        {
            GameObject.Find("Player").GetComponent<Player>().SetMaxEffect(playerAsset.MaxEffectValue + objectsAsset.MaxEffectValue);
        }

        if (objectsAsset.Speed != 0)
            playerAsset.Speed *= objectsAsset.Speed;

    }

    /*public void ActiveChange()
    {
        playerAsset.Hp += item.HP;
        playerAsset.MaxHP += item.MaxHP;
        playerAsset.EffectValue += item.EffectValue;
        playerAsset.MaxEffectValue += item.MaxEffectValue;
        playerAsset.Speed += item.Speed;

        if (item.Duration != 0)
        {
            StartCoroutine(Duration(item, item.Duration));
        }
    }*/

    IEnumerator Duration(ObjectsAsset item, uint duration)
    {
        yield return new WaitForSeconds(duration);
        playerAsset.Hp -= item.HP;
        playerAsset.MaxHP -= item.MaxHP;
        playerAsset.EffectValue -= item.EffectValue;
        playerAsset.MaxEffectValue -= item.MaxEffectValue;
        playerAsset.Speed -= item.Speed;
    }
}
