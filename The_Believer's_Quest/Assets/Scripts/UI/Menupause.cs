using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menupause : MonoBehaviour
{
    public static bool show = false;
    public GameObject canvaspause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (show)
                Resume();
            else
                Pause();
        }
    }

    void Resume()
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
}
