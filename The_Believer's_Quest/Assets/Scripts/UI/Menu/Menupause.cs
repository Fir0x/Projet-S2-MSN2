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
    public GameObject originWeapon;

    private void Awake()
    {
        canvaspause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (show)
            {
                Resume();
                canvaspause.SetActive(false);
            }
            else
            {
                canvaspause.SetActive(true);
                Pause();
            }
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
        Time.timeScale = 1f;
        Destroy(Player.instance.PlayerAsset.WeaponsList[0]);
        Destroy(Player.instance.PlayerAsset.WeaponsList[1]);
        Player.instance.PlayerAsset.WeaponsList[0] = Instantiate(originWeapon);
        Player.instance.PlayerAsset.WeaponsList[0] = null;
        SceneManager.LoadScene("MainMenu");
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeBO(0);
        show = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
