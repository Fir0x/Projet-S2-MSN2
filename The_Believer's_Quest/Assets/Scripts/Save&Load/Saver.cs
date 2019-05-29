using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//Nicolas I
public static class Saver
{
    [Serializable]
    public class PlayerSettings
    {
        public int BGMvolume; //Background music volume
        public int BGSvolume; //Background sounds volume

        public PlayerSettings(int BGMvolume, int BGSvolume)
        {
            this.BGMvolume = BGMvolume;
            this.BGSvolume = BGSvolume;
        }
    }

    [Serializable]
    public class PlayerSave
    {
        public int floor;
        public int hp;
        public int maxHP;
        public int effectValue;
        public int maxEffectValue;
        public float speed;
        public int gold;
        public int diamond;
        public string weaponsList;
        public List<GameObject> unlockedItems;
        public UnityEngine.Random.State seed;

        public PlayerSave(PlayerAsset data, List<GameObject> unlockedItems, UnityEngine.Random.State seed)
        {
            floor = data.Floor;
            hp = data.Hp;
            maxHP = data.MaxHP;
            effectValue = data.EffectValue;
            maxEffectValue = data.MaxEffectValue;
            speed = data.Speed;
            gold = data.Gold;
            diamond = data.Diamond;
            weaponsList = data.SerializeWeapons();
            this.unlockedItems = unlockedItems;
            this.seed = seed;
        }
    }

    private static string SerializeItems(List<GameObject> unlockedItems)
    {
        string serilized = "";
        int nbUnlocked = unlockedItems.Count;
        Object obj;
        for (int i = 0; i < nbUnlocked - 1; i++)
        {
            if ((obj = unlockedItems[i].GetComponent<Object>()) != null)
                serilized += "Object/" + obj.name + ",";
            else
                serilized += "Weapon/" + unlockedItems[i].GetComponent<Weapon>().name + ",";
        }

        if ((obj = unlockedItems[nbUnlocked - 1].GetComponent<Object>()) != null)
            serilized += "Object/" + obj.name;
        else
            serilized += "Weapon/" + unlockedItems[nbUnlocked - 1].GetComponent<Weapon>().name;

        return serilized;
    }

    private static void Saving(PlayerSave save)
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        Debug.Log("Save file path: " + path); //DEBUG
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    private static void Saving(PlayerSettings save)
    {

        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerSettings.bin");
        Debug.Log("Save file path: " + path); //DEBUG
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    public static void SavePlayerData(PlayerAsset playerData, List<GameObject> unlockedItems, UnityEngine.Random.State seed)
    {
        Saving(new PlayerSave(playerData, new List<GameObject>(), seed));
    }

    public static void SavePlayerSettings(int BGMvolume, int BGSvolume)
    {
        Saving(new PlayerSettings(BGMvolume, BGSvolume));
    }
}
