using System.Threading;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
        private AudioSource BO;
    
        public AudioSource efxSource;                   
        public AudioSource musicmenu; 
        public AudioSource fstlvl;
        public AudioSource fstlvlBoss;

        private AudioSource[] musics;
        public static SoundManager instance = null;      
        
        
        void Awake ()
        {
            musics = new[] {musicmenu, fstlvl, fstlvlBoss};
            
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
            
            DontDestroyOnLoad(gameObject);
        }
        
        public void PlaySingle(AudioClip clip)
        {
            efxSource.clip = clip;
            efxSource.Play ();
        }
        
        public void RandomizeSfx (params AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            efxSource.clip = clips[randomIndex];
            efxSource.Play();
        }
        
        public void ChangeBO(int x)
        {
            musicmenu.Stop();
            Thread.Sleep(300);
            musicmenu.clip = musics[x].clip;
            musicmenu.Play();
        }
}
    