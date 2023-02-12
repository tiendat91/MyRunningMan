using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PathFollow
{
    /// <summary>
    /// 
    /// </summary>
    public bool CollidingWithPlayer { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<HeroController>() != null)
        {
            CollidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollidingWithPlayer = false;
    }
}
