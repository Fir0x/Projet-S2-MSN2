
using UnityEngine;
//Sarah
public class Gameover : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject sliderBoss;

    public GameObject canvasgamover;
    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }
    private bool debug;


    public void Return_to_hub()
    {
        playerAsset.Hp = 100;
        playerAsset.MaxHP = 100;
        Player.instance.SetEffect(0);
        playerAsset.MaxEffectValue = 50;
        playerAsset.Gold = 0;
        playerAsset.WeaponsList = new[] { pistol, null };
        playerAsset.WeaponsList[0].GetComponent<WeaponItem>().WeaponAsset.Ammunitions = 50;
        playerAsset.ObjectsList[0] = null;
        playerAsset.ObjectsList[1] = null;
        playerAsset.Floor = -1;
        playerAsset.Invicibility = false;
        playerAsset.Speed = 3;
        Inventory.instance.items.Clear();
        UIController.uIController.changeHp.Invoke();
        UIController.uIController.changeMaxHp.Invoke();
        UIController.uIController.changeGold.Invoke();
        UIController.uIController.changeDiamond.Invoke();
        UIController.uIController.changeAmmo.Invoke();
        UIController.uIController.changeWeapon.Invoke();
        UIController.uIController.changeEffect.Invoke();
        UIController.uIController.changeMaxEffect.Invoke();

        Destroy(GameObject.Find("Board"));
        GameObject.FindGameObjectWithTag("BoardManager").GetComponent<Board>().Init();
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(1);
        MapController.mapInstance.ResetMap();

        canvasgamover.SetActive(false);
        sliderBoss.SetActive(false);
    }

    public void QUIT_GAME()
    {
        Application.Quit();
    }

}
