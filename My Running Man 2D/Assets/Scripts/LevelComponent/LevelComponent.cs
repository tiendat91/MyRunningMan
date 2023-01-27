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
            if (instantKill)
            {
                //kill player immediately
                player.GetComponent<Health>().KillPlayer();
            }
            else
            {
                player.GetComponent<Health>().LoseLife();
            }

        }
    }
}
