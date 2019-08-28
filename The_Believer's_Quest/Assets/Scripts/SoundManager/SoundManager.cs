using System.Collections.Generic;
using System.Threading;
using UnityEngine;
//Maxence && Nicolas
public class SoundManager : MonoBehaviour
{
    public AudioSource currentMusic;
    public AudioSource currentFx;

    [SerializeField] MusicAsset Musics;
    AudioClip[] musics;

    public List<AudioClip> lfx;

    void Start ()
    {
        musics = Musics.Musics;
        if (Player.instance != null && Player.instance.PlayerAsset.Floor != 0)
            ChangeBO(Player.instance.PlayerAsset.Floor + 3);
    }

    public void PlaySingle(AudioClip clip)
    {
        currentFx.clip = clip;
        currentFx.Play();
    }

    public void ChangeVolumeMusic(float volume)
    {
        currentMusic.volume = (80f + volume) / 80f;
    }

    public void ChangeVolumeFx(float volume)
    {
        currentFx.volume = (80f + volume) / 80f;
    }

    public void ChangeBO(int x)
    {
        currentMusic.Stop();
        Thread.Sleep(300);
        currentMusic.clip = musics[x];
        currentMusic.Play();
    }
}
    