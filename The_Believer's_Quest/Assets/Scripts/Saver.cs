using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Saver : MonoBehaviour
{
    private readonly string path;
    private Player player;
    private Floor floor;

    public Saver(Player player, Floor floor)
    {
        this.player = player;
        this.floor = floor;
    }

    private void Saving()
    {
        print("Saving process begins");
        IFormatter formatter = new BinaryFormatter();
        //Binary save file opening
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, this); //Saving of the Saver object in the binary file
        stream.Close();
        print("Save is successful.");
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
