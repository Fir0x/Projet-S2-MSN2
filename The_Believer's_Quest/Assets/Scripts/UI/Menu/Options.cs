using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    //Sarah
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private SoundManager soundManager;

    public AudioMixer AudioMixer { get => audioMixer; set => audioMixer = value; }
    public Slider SoundSlider { get => soundSlider; set => soundSlider = value; }
    public Slider MusicSlider { get => musicSlider; set => musicSlider = value; }

    private void Start() //Nicolas I
    {
        if (File.Exists("playerSettings.bin"))
        {
            soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
            Tuple<float,float> soundInfos =  Loader.LoadingPlayerSettings();
            VolumeEffect(soundInfos.Item1);
            VolumeMusic(soundInfos.Item2);
            soundSlider.value = soundInfos.Item1;
            musicSlider.value = soundInfos.Item2;
        }
        else
        {
            float BGS;
            float BGM;
            audioMixer.GetFloat("Sound", out BGS);
            audioMixer.GetFloat("Music", out BGM);
            Saver.SavePlayerSettings(BGS, BGM);
        }
    }

    public void VolumeMusic(float volume)
    {
        AudioMixer.SetFloat("Music", volume);
        soundManager.ChangeVolumeMusic(volume);
    }

    public void VolumeEffect(float volume)
    {
        AudioMixer.SetFloat("Sound", volume);
        soundManager.ChangeVolumeFx(volume);
    }

    public void SaveChange()
    {
        float BGS;
        float BGM;
        audioMixer.GetFloat("Sound", out BGS);
        audioMixer.GetFloat("Music", out BGM);
        Saver.SavePlayerSettings(BGM, BGS);
    }
}
