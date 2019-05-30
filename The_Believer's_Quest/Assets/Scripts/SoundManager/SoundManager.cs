using System.Threading;
using UnityEngine;
//Maxence
public class SoundManager : MonoBehaviour
{
    public AudioSource currentMusic;

    [SerializeField] MusicAsset Musics;
    AudioClip[] musics;
    public static SoundManager instance = null;


    void Start ()
    {
        musics = Musics.Musics;
        PlaySingle(currentMusic.clip);
    }

    public void PlaySingle(AudioClip clip)
    {
        currentMusic.clip = clip;
        currentMusic.Play ();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        currentMusic.clip = clips[randomIndex];
        currentMusic.Play();
    }

    public void ChangeBO(int x)
    {
        currentMusic.Stop();
        Thread.Sleep(300);
        currentMusic.clip = musics[x];
        currentMusic.Play();
    }
}
    