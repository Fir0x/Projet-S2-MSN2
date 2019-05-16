using System;
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

    public static void LoadingPlayerData(PlayerAsset playerData)
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //Loading the GameSave object from the binary file
        Saver.PlayerSave save = (Saver.PlayerSave) new BinaryFormatter().Deserialize(streamRestauration);
        playerData.Diamond = save.diamond;
        //FIX ME: En attente du système de débloquage d'arme
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
