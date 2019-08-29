using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Nicolas I && Maxence
public static class Loader
{
    public static void LoadingPlayerData(ref PlayerAsset playerData, ref UnlockedItemsAsset unlockedItems)
    {
        string path;
        if (Application.isEditor)
            path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        else
            path = Path.Combine(Application.persistentDataPath, "playerData.bin");
        Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //Loading the GameSave object from the binary file
        Saver.PlayerSave save = (Saver.PlayerSave) new BinaryFormatter().Deserialize(streamRestauration);
        string test = save.playerData;
        string test2 = save.unlockedItems;
        JsonUtility.FromJsonOverwrite(save.playerData, playerData);
        JsonUtility.FromJsonOverwrite(save.unlockedItems, unlockedItems);

        streamRestauration.Close();

        //GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(.GetComponent<Player>().PlayerAsset.Floor + 2);
    }

    public static Tuple<float, float> LoadingPlayerSettings()
    {
        string path;
        if (Application.isEditor)
            path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerSettings.bin");
        else
            path = Path.Combine(Application.persistentDataPath, "playerSettings.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        Saver.PlayerSettings save = (Saver.PlayerSettings)new BinaryFormatter().Deserialize(streamRestauration);
        streamRestauration.Close();
        return new Tuple<float, float>(save.BGSvolume, save.BGMvolume);
    }
}
