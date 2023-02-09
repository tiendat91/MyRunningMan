using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroConditions : MonoBehaviour
{
    public bool IsCollidingBelow { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsFalling { get; set; }
    public bool IsWallCling { get; set; }
    public bool IsJetPacking { get; set; }


    public void Reset()
    {
        IsCollidingBelow = false;
        IsCollidingLeft = false;
        IsCollidingRight = false;
        IsCollidingAbove = false;
        IsFalling = false;
    }

}
