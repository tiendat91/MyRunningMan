using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float friction = 0.1f; //luc ma sat

    public float Friction => friction;


}
