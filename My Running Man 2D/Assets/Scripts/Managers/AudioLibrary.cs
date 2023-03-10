using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip collectableClip;
    [SerializeField] private AudioClip projecttileCLip;
    [SerializeField] private AudioClip enemyProjecttileCLip;
    [SerializeField] private AudioClip playerDeadClip;
    [SerializeField] private AudioClip projectileCollisionCLip;


    public AudioClip JumpClip => jumpClip;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip ProjectileClip => projecttileCLip;
    public AudioClip EnemyProjectileClip => enemyProjecttileCLip;
    public AudioClip PlayerDeadClip => playerDeadClip;
    public AudioClip ProjectileCollisionClip => projectileCollisionCLip;
}
