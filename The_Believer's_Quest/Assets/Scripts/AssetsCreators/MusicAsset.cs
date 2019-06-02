using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maxence && Nicolas L
[CreateAssetMenu(fileName = "MusicAsset", menuName = "Musics")]
public class MusicAsset : ScriptableObject
{
    [SerializeField] private AudioClip[] musics;

    public AudioClip[] Musics { get => musics; set => musics = value; }
}