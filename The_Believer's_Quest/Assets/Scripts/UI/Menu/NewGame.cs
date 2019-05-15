using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerData;

    public PlayerAsset PlayerData { get => playerData; set => playerData = value; }

    public void StartNewGame()
    {
        Saver.SavePlayerData(playerData);
    }
}
