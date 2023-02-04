using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroConditions : MonoBehaviour
{
    public bool IsCollidingBelow { get; set; }
    public bool IsCollidingAbove { get; set; }

    public bool IsFalling { get; set; }
    public void Reset()
    {
        IsCollidingBelow = false;
        IsFalling = false;
    }

}
