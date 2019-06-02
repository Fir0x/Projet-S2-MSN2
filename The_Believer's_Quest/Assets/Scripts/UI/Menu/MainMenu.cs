using UnityEngine;
using UnityEngine.SceneManagement;
//Sarah
public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerAsset playerAsset;
    [SerializeField] public UnlockedItemsAsset unlockedItems;

    public PlayerAsset PlayerAsset { get => playerAsset; set => playerAsset = value; }

    public void START_GAME()
    {    
        SceneManager.LoadScene("SampleScene");
    }

    public void QUIT_GAME()
    {
       Application.Quit(); 
    }

    public void CONTINUE()
    {
        Loader.LoadingPlayerData(ref playerAsset, ref unlockedItems);
        START_GAME();
    }

}
