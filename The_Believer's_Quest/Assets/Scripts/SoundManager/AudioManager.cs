using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BO;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeBO(AudioClip music)
    {
        if (BO.clip.name == music.name) return;
        
        BO.Stop();
        BO.clip = music;
        BO.Play();
    }
}
