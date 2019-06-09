
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


    public void ReturnToHub()
    {
        Player.instance.RestartPlayer();
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