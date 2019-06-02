
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private WeaponAsset sword;
    [SerializeField] private GameObject sliderBoss;

    public GameObject canvasgamover;
    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }

    public void Return_to_hub()
    {
        
        playerAsset.Hp = 100;
        playerAsset.MaxHP = 100;
        playerAsset.EffectValue = 0;
        playerAsset.MaxEffectValue = 0;
        playerAsset.Gold = 0;
        playerAsset.Diamond = 0;
        playerAsset.WeaponsList = new []{sword, null};
        playerAsset.Floor = -1;
        playerAsset.Invicibility = false;
        UIController.uIController.changeHp.Invoke();
        UIController.uIController.changeMaxHp.Invoke();
        UIController.uIController.changeGold.Invoke();
        UIController.uIController.changeDiamond.Invoke();
        UIController.uIController.changeAmmo.Invoke();
        UIController.uIController.changeWeapon.Invoke();
        UIController.uIController.changeEffect.Invoke();
        UIController.uIController.changeMaxEffect.Invoke();

        GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(1);
        Destroy(GameObject.Find("Board"));
        MapController.mapInstance.ResetMap();

        canvasgamover.SetActive(false);
        sliderBoss.SetActive(false);
    }

    public void QUIT_GAME()
    {
        Application.Quit(); 
    }

}
