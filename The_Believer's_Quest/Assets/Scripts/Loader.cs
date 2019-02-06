using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private readonly string path;
    private Player player;
    private Floor floor;

    public void Loading(string path)
    {
        IFormatter formatterRestauration = new BinaryFormatter();
        //Binary save file opening
        Stream streamRestauration = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //Loading the GameSave object from the binary file
        Saver save = (Saver)formatterRestauration.Deserialize(streamRestauration);
        streamRestauration.Close();

        //All Game's attributes are stocked in one array in order to make their transfer more easy to do
        player = save.GetPlayer();
        floor = save.GetFloor();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Floor GetFloor()
    {
        return floor;
    }
}
