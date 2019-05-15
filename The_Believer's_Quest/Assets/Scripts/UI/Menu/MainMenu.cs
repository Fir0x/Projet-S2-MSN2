using UnityEngine;
using UnityEngine.SceneManagement;
//Sarah
public class MainMenu : MonoBehaviour
{
    public void START_GAME()
    {    
        SceneManager.LoadScene("SampleScene");
        SoundManager.instance.ChangeBO(1);
    }

    public void QUIT_GAME()
    {
       Application.Quit(); 
    }


}
