using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] protected bool instantKill;
    public virtual void Damage(HeroMotor player)
    {
        if (player != null)
        {
                //kill player immediately
                DeathManager.Instance.AddDeaths();
                player.GetComponent<Health>().KillPlayer();
        }
    }
}
