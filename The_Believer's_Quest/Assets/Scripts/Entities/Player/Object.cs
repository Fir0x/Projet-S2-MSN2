using System.Collections;
using UnityEngine;
//Nicolas L
public class Object : MonoBehaviour
{
    [SerializeField] private ObjectsAsset objectsAsset;

    public ObjectsAsset ObjectsAsset { get => objectsAsset; set => objectsAsset = value; }

    PlayerAsset playerAsset;
    private bool cooldown;
    int life;

    public void PassiveChange()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        playerAsset = player.PlayerAsset;

        if (objectsAsset.HP != 0)
            player.SetLife(playerAsset.Hp + objectsAsset.HP);

        if (objectsAsset.MaxHP != 0)
            player.SetMaxLife(playerAsset.MaxHP + objectsAsset.MaxHP);
        
        if (objectsAsset.EffectValue != 0)
            player.SetEffect(playerAsset.EffectValue + objectsAsset.EffectValue);

        if (objectsAsset.MaxEffectValue != 0)
            player.SetMaxEffect(playerAsset.MaxEffectValue + objectsAsset.MaxEffectValue);

        if (objectsAsset.Speed != 0)
            playerAsset.Speed *= objectsAsset.Speed;

    }

    public void ActiveChange()
    {
        print("Active CHange");

        GameObject player = GameObject.Find("Player");
        playerAsset = player.GetComponent<Player>().PlayerAsset;

        if (objectsAsset.HP == -999)
        {
            player.GetComponent<Player>().SetLife(1);
            player.GetComponentInChildren<Weapon>().MultiplyDamage(2);
        }

        if (objectsAsset.HP != 0 && objectsAsset.HP != -999)
            player.GetComponent<Player>().SetLife(playerAsset.Hp + objectsAsset.HP);

        if (objectsAsset.MaxHP != 0)
            player.GetComponent<Player>().SetMaxLife(playerAsset.MaxHP + objectsAsset.MaxHP);

        if (objectsAsset.EffectValue != 0)
            player.GetComponent<Player>().SetEffect(playerAsset.EffectValue + objectsAsset.EffectValue);

        if (objectsAsset.MaxEffectValue != 0)
            player.GetComponent<Player>().SetMaxEffect(playerAsset.MaxEffectValue + objectsAsset.MaxEffectValue);

        if (objectsAsset.Speed != 0)
            playerAsset.Speed *= objectsAsset.Speed;

        if (objectsAsset.Invicibility)
            playerAsset.Invicibility = true;

        if (objectsAsset.Ammo != 0)
            player.GetComponentInChildren<Weapon>().MultiplyDamage(2);

        if (objectsAsset.Duration != 0)
            Instantiate(gameObject).GetComponent<Object>().StartDuration(player, objectsAsset, objectsAsset.Duration); //Instantiate to use coroutine
        
        Inventory.instance.Remove(gameObject);
    }

    public void StartDuration(GameObject player, ObjectsAsset item, uint duration)
    {
        StartCoroutine(Duration(player, objectsAsset, objectsAsset.Duration));
    }

    IEnumerator Duration(GameObject player, ObjectsAsset item, uint duration) //applies object's effects for a duration
    {
        yield return new WaitForSeconds(duration);
        PlayerAsset playerAsset = player.GetComponent<Player>().PlayerAsset;
        playerAsset.Hp -= item.HP;
        playerAsset.MaxHP -= item.MaxHP;
        playerAsset.EffectValue -= item.EffectValue;
        playerAsset.MaxEffectValue -= item.MaxEffectValue;
        playerAsset.Speed /= item.Speed;
        playerAsset.Invicibility = false;
        if (objectsAsset.HP == -999) //AFIT object's effect
            player.GetComponentInChildren<Weapon>().MultiplyDamage(0.5f);

        Destroy(gameObject); //Destroy trash gameObject's instance
    }
}
