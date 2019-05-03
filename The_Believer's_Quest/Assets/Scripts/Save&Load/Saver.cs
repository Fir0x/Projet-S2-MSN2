using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//Nicolas I
public class Saver : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerData;

    public PlayerAsset PlayerData { get => playerData; set => playerData = value; }

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
        public int diamond;
        public List<int> unlockedWeapons = new List<int>();

        public PlayerSave(int diamond, List<int> unlockedWeapons)
        {
            this.diamond = diamond;
            this.unlockedWeapons = unlockedWeapons;
        }
    }

    private void Saving(PlayerSave save, string filename)
    {

        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), filename);
        //print("File path: " + path); //DEBUG
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    private void Saving(PlayerSettings save, string filename)
    {

        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), filename);
        //print("File path: " + path); //DEBUG
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        new BinaryFormatter().Serialize(stream, save); //Saving of the GameSave object in the binary file
        stream.Close();
    }

    public void SavePlayerData()
    {
        Saving(new PlayerSave(PlayerData.Diamond, new List<int>()), "playerData");
    }

    public void SavePlayerSettings()
    {
        Saving(new PlayerSettings(0, 0), "playerSettings");
    }
}
