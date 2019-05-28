﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Sarah
public class Menupause : MonoBehaviour
{
    //cette classe a été réalisé par Sarah
    public static bool show = false;
    public GameObject canvaspause;

    private void Awake()
    {
        canvaspause.SetActive(false);
    }

    void FixedUpdate()
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
        SceneManager.LoadScene("MainMenu");
        show = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
