using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonControl : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]private GameObject playerPrefab;

    void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void GoLeft()
    {
        playerPrefab.GetComponent<HeroStates>().GoLeft();
    }
    public void GoRight()
    {
        playerPrefab.GetComponent<HeroStates>().GoRight();
    }
    public void UnpressButton()
    {
        playerPrefab.GetComponent<HeroStates>().UnpressButton();
    }
    public void Jump()
    {
        playerPrefab.GetComponent<HeroJump>().Jump();
    }
    

}
