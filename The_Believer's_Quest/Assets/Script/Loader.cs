using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private readonly string path;
    private Player player;
    private Board board;

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
        board = save.GetBoard();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Board GetFloor()
    {
        return board;
    }
}
