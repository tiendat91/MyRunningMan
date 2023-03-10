using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : Singleton<KeyManager>
{
    public int TotalKeys { get; set; }
    private string KEY_KEYS = "MyGame_TOTAL_KEYS";

    private void Start()
    {
        PlayerPrefs.SetInt(KEY_KEYS, 0);
        LoadCoins();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddKeys(1);
        }
    }

    private void LoadCoins()
    {
        TotalKeys = PlayerPrefs.GetInt(KEY_KEYS, 0);
    }

    public void AddKeys (int amount)
    {
        TotalKeys += amount;
        PlayerPrefs.SetInt(KEY_KEYS, TotalKeys);
        PlayerPrefs.Save();
    }

    public void RemoveKeys (int amount)
    {
        TotalKeys -= amount;

        PlayerPrefs.SetInt(KEY_KEYS, TotalKeys);
        PlayerPrefs.Save();
    }
}
