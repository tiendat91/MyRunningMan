using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip collectableClip;
    [SerializeField] private AudioClip playerDeadClip;


    public AudioClip JumpClip => jumpClip;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip PlayerDeadClip => playerDeadClip;
}
