using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    //Sarah
    public AudioMixer audioMixer;
    
    public void VolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().ChangeVolume(volume);
    }
    public void VolumeEffect(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }


}
