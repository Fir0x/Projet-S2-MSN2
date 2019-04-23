using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Sarah
public class Menupause : MonoBehaviour
{
    //cette classe a été réalisé par Sarah
    public static bool show = false;
    public GameObject canvaspause;

    void Update()
    {
        if (!show)
            canvaspause.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (show)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        canvaspause.SetActive(false);
        Time.timeScale = 1f;
        show = false;
    }

    void Pause()
    {
        canvaspause.SetActive(true);
        Time.timeScale = 0f;
        show = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        show = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SaveGame()
    {
        //FIX ME
    }
}
