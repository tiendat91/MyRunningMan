using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : Singleton<DeathManager>
{
    public int TotalDeaths { get; set; }
    private string KEY_DEATHS = "MyGame_TOTAL_DEATHS";

    private void Start()
    {
        PlayerPrefs.SetInt(KEY_DEATHS, 0);
        LoadDeaths();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadDeaths()
    {
        TotalDeaths = PlayerPrefs.GetInt(KEY_DEATHS, 0);
    }

    public void AddDeaths()
    {
        TotalDeaths += 1;
        PlayerPrefs.SetInt(KEY_DEATHS, TotalDeaths);
        PlayerPrefs.Save();
    }
}
