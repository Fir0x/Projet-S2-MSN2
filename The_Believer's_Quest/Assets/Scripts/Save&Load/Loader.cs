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

    public static UnityEngine.Random.State LoadingPlayerData(ref PlayerAsset playerData, ref UnlockedItemsAsset unlockedItems)
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerData.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //Loading the GameSave object from the binary file
        Saver.PlayerSave save = JsonUtility.FromJson<Saver.PlayerSave>((string)new BinaryFormatter().Deserialize(streamRestauration));
        playerData = save.playerData;
        foreach (GameObject item in save.unlockedItems)
        {
            if (unlockedItems.Locked.Contains(item))
            {
                unlockedItems.Unlocked.Add(item);
                unlockedItems.Locked.Remove(item);
            }
        }
        //GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(.GetComponent<Player>().PlayerAsset.Floor + 2);

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

    public static Tuple<float, float> LoadingPlayerSettings()
    {
        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "playerSettings.bin");
        //Debug.Log("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        Saver.PlayerSettings save = (Saver.PlayerSettings)new BinaryFormatter().Deserialize(streamRestauration);
        return new Tuple<float, float>(save.BGSvolume, save.BGMvolume);
    }
}
