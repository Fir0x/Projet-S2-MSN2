using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//Nicolas I
public static class Saver
{
    [Serializable]
    public class PlayerSettings
    {
        public float BGMvolume; //Background music volume
        public float BGSvolume; //Background sounds volume

        public PlayerSettings(float BGMvolume, float BGSvolume)
        {
            this.BGMvolume = BGMvolume;
            this.BGSvolume = BGSvolume;
        }
    }

    [Serializable]
    public class PlayerSave
    {
        public string playerData;
        public string unlockedItems;

        public PlayerSave(PlayerAsset playerData, UnlockedItemsAsset unlockedItems)
        {
            this.playerData = JsonUtility.ToJson(playerData);
            this.unlockedItems = JsonUtility.ToJson(unlockedItems);
        }
    }

    private static void Saving(PlayerSave save)
    {
        string path;
        if (Application.isEditor)
            path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        else
            path = Path.Combine(Application.persistentDataPath, "playerData.bin");
        Debug.Log("Save file path: " + path); //DEBUG
        if (!File.Exists(path)) //Create file if does not exist (avoid error in game after installation)
            File.Create(path).Close();

        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    private static void Saving(PlayerSettings save)
    {
        string path;
        if (Application.isEditor)
            path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerSettings.bin");
        else
            path = Path.Combine(Application.persistentDataPath, "playerSettings.bin");
        Debug.Log("Save file path: " + path); //DEBUG
        if (!File.Exists(path)) //Create file if does not exist (avoid error in game after installation)
            File.Create(path).Close();

        //Binary save file opening
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    public static void SavePlayerData(PlayerAsset playerData, UnlockedItemsAsset unlockedItems)
    {
        Saving(new PlayerSave(playerData, unlockedItems));
    }

    public static void SavePlayerSettings(float BGMvolume, float BGSvolume)
    {
        Saving(new PlayerSettings(BGMvolume, BGSvolume));
    }
}
