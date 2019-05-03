using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Nicolas I
public class Loader : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerData;

    public PlayerAsset PlayerData { get => playerData; set => playerData = value; }

    private enum ISaveType
    {
        Data,
        Settings
    };

    private void Loading(string filename, ISaveType saveType)
    {
        if (!filename.Contains(".json"))
            throw new Exception("Abnormal save file");

        string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), filename);
        //print("File path: " + path); //DEBUG
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        if (saveType == ISaveType.Data)
        {
            //Loading the GameSave object from the binary file
            Saver.PlayerSave save = (Saver.PlayerSave) new BinaryFormatter().Deserialize(streamRestauration);
            playerData.Diamond = save.diamond;
            //FIX ME: En attente du système de débloquage d'arme
        }
        else
        {
            Saver.PlayerSettings save = (Saver.PlayerSettings) new BinaryFormatter().Deserialize(streamRestauration);
            //FIX ME: En attente de la gestion du niveau sonore
        }
    }
}
