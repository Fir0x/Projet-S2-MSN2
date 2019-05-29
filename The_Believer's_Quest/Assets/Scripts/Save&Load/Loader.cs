using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Nicolas I
public static class Loader
{

    private enum ISaveType
    {
        Data,
        Settings
    };

    public static Random.State LoadingPlayerData(ref PlayerAsset playerData, ref UnlockedItemsAsset unlockedItems)
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //Loading the GameSave object from the binary file
        Saver.PlayerSave save = (Saver.PlayerSave) new BinaryFormatter().Deserialize(streamRestauration);
        playerData.Floor = save.floor;
        playerData.Hp = save.hp;
        playerData.MaxHP = save.maxHP;
        playerData.EffectValue = save.effectValue;
        playerData.MaxEffectValue = save.maxEffectValue;
        playerData.Speed = save.speed;
        playerData.Gold = save.gold;
        playerData.Diamond = save.diamond;
        string[] serializedWeapons = save.weaponsList.Split(',');
        playerData.WeaponsList[0] = Resources.Load<WeaponAsset>("Weapons/" + serializedWeapons[0]);
        playerData.WeaponsList[1] = Resources.Load<WeaponAsset>("Weapons/" + serializedWeapons[1]);
        foreach (GameObject item in save.unlockedItems)
        {
            if (unlockedItems.Locked.Contains(item))
            {
                unlockedItems.Unlocked.Add(item);
                unlockedItems.Locked.Remove(item);
            }
        }

        return save.seed;
    }

    private static GameObject[] DeserializeItem(string serialized)
    {
        string[] split = serialized.Split(',');
        GameObject[] result = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            string[] infos = split[i].Split('/');
            result[i] = infos[0] == "Object" ? Resources.Load<GameObject>("Items/" + infos[1]) : Resources.Load<GameObject>("Items/" + infos[1]);
        }

        return result;
    }

    public static void LoadingPlayerSettings()
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerSettings.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        Saver.PlayerSettings save = (Saver.PlayerSettings)new BinaryFormatter().Deserialize(streamRestauration);
        //FIX ME: En attente de la gestion du niveau sonore
    }
}
