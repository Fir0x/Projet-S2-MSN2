using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas I
public class NewGame : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerData;

    public PlayerAsset PlayerData { get => playerData; set => playerData = value; }

    public void StartNewGame()    
    {
        Saver.SavePlayerData(playerData, new List<GameObject>());
    }
}
