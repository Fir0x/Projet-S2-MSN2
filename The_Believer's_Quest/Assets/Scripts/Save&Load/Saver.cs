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
        public PlayerAsset playerData;
        public List<GameObject> unlockedItems;

        public PlayerSave(PlayerAsset playerData, List<GameObject> unlockedItems)
        {
            this.playerData = playerData;
            this.unlockedItems = unlockedItems;
        }
    }

    private static void Saving(PlayerSave save)
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        Debug.Log("Save file path: " + path); //DEBUG
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, JsonUtility.ToJson(save)); //Saving of the GameSave object in the binary file
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

    public static void SavePlayerData(PlayerAsset playerData, List<GameObject> unlockedItems)
    {
        Saving(new PlayerSave(playerData, unlockedItems));
    }

    public static void SavePlayerSettings(float BGMvolume, float BGSvolume)
    {
        Saving(new PlayerSettings(BGMvolume, BGSvolume));
    }
}
