﻿using System;
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
        public PlayerAsset playerData;
        public List<GameObject> unlockedItems;
        public UnityEngine.Random.State seed;

        public PlayerSave(PlayerAsset playerData, List<GameObject> unlockedItems, UnityEngine.Random.State seed)
        {
            this.playerData = playerData;
            this.unlockedItems = unlockedItems;
            this.seed = seed;
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

    public static void SavePlayerData(PlayerAsset playerData, List<GameObject> unlockedItems, UnityEngine.Random.State seed)
    {
        Saving(new PlayerSave(playerData, new List<GameObject>(), seed));
    }

    public static void SavePlayerSettings(int BGMvolume, int BGSvolume)
    {
        Saving(new PlayerSettings(BGMvolume, BGSvolume));
    }
}
