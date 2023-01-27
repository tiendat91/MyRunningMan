using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : LevelComponent
{
    public override void Damage(HeroMotor player)
    {
        base.Damage(player);
        Debug.Log("Rock");
    }

}
