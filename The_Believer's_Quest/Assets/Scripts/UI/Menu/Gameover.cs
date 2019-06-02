using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private WeaponAsset sword;
    public GameObject canvasgamover;
    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }

    public void Return_to_hub()
    {
        canvasgamover.SetActive(false);
        playerAsset.Hp = 100;
        playerAsset.MaxHP = 100;
        playerAsset.EffectValue = 0;
        playerAsset.MaxEffectValue = 0;
        playerAsset.Gold = 0;
        playerAsset.Diamond = 0;
        playerAsset.WeaponsList = new []{sword};
        playerAsset.Floor = -1;
        
        GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(1);
        Destroy(GameObject.Find("Board"));
        MapController.mapInstance.ResetMap();
        
        playerAsset.Invicibility = false;
    }

    public void QUIT_GAME()
    {
        Application.Quit(); 
    }

}
