
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
        Player.instance.PlayerAsset.Hp = 100;
        Player.instance.PlayerAsset.MaxHP = 100;
        Player.instance.SetEffect(0);
        Player.instance.PlayerAsset.MaxEffectValue = 50;
        Player.instance.PlayerAsset.Gold = 0;
        Player.instance.PlayerAsset.WeaponsList[0].GetComponent<WeaponItem>().ResetAsset();
        Player.instance.PlayerAsset.WeaponsList[0].GetComponent<WeaponItem>().ResetAsset();
        Player.instance.PlayerAsset.WeaponsList = new[] { pistol, null };
        Player.instance.PlayerAsset.ObjectsList[0] = null;
        Player.instance.PlayerAsset.ObjectsList[1] = null;
        Player.instance.PlayerAsset.Floor = -1;
        Player.instance.PlayerAsset.Invicibility = false;
        Player.instance.PlayerAsset.Speed = 3;
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