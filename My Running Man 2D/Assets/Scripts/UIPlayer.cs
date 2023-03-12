using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class UIPlayer : MonoBehaviour
{


    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject optionsSettings;
    [SerializeField] private TextMeshProUGUI timeCount;
    [SerializeField] private TextMeshProUGUI keyCount;
    [SerializeField] private TextMeshProUGUI deathCount;
    [SerializeField] public AudioSource _audio;
    [SerializeField] private UnityEngine.TextAsset _textAsset;

    [SerializeField] private float timeCounting = 10;
    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private int numberOfDeath = 0;
    [SerializeField] private int keyCollected = 0;


    public List<Account> Accounts { get; set; }

    private string fileName = null;
    private static List<Account> accounts = new List<Account>();

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        fileName = Application.dataPath + "/Data/Account_Player.csv";

        panelSettings.SetActive(false);
        optionsSettings.SetActive(false);
        //ReadData();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape)) {
        //    TurnOnPauseMenu(true);
        //}

        if (timerIsRunning)
        {
            if (timeCounting >= 0)
            {
                timeCounting += Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeCounting = 0;
                timerIsRunning = false;
            }
        }

        timeCount.text = TimeSpan.FromSeconds(timeCounting).ToString("hh':'mm':'ss");
        UpdateKeys();
        UpdateDeaths();
    }
        
    public void TurnOnPauseMenu(bool isOn)
    {
        if (isOn)
        {
            Time.timeScale = 0f;
            panelSettings.SetActive(true);
        }
        else
        {
            panelSettings.SetActive(false);
            optionsSettings.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void TurnOnOptionMenu(bool isOn)
    {
        if (isOn)
        {
            panelSettings.SetActive(false);
            optionsSettings.SetActive(true);
        }
        else
        {
            panelSettings.SetActive(true);
            optionsSettings.SetActive(false);
        }
    }

    public void TurnOnOffAudio(bool isCheck)
    {
        _audio.GetComponent<AudioSource>().mute = isCheck;
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateKeys()
    {
        keyCount.text = KeyManager.Instance.TotalKeys.ToString(); 
    }

    public void UpdateDeaths()
    {
        deathCount.text = "X "+ DeathManager.Instance.TotalDeaths.ToString();
    }

    //public void ReadData()
    //{
    //    string[] data = _textAsset.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
    //    int NumberOfPropertiesInData = typeof(Account).GetProperties().Length;
    //    int tableSize = data.Length / NumberOfPropertiesInData - 1;
    //    for (int i = NumberOfPropertiesInData; i < data.Length - NumberOfPropertiesInData; i += NumberOfPropertiesInData)
    //    {
    //        accounts.Add(new Account()
    //        {
    //            Name = data[i],
    //            Password = data[i + 1],
    //            Score = Int32.Parse(data[i + 2]),
    //            NumberOfDeath = Int32.Parse(data[i + 3]),
    //            Level1 = float.Parse(data[i + 5]),
    //            Level2 = float.Parse(data[i + 6]),
    //            Level3 = float.Parse(data[i + 7]),
    //            Level4 = float.Parse(data[i + 8]),
    //            Level5 = float.Parse(data[i + 9]),
    //            Level6 = float.Parse(data[i + 10]),

    //            TimePLaying = float.Parse(data[i + 5])
    //            + float.Parse(data[i + 6])
    //            + float.Parse(data[i + 7])
    //            + float.Parse(data[i + 8])
    //            + float.Parse(data[i + 9])
    //            + float.Parse(data[i + 10])
    //        });
    //    }
    //}

    public void WriteDataWithTime()
    {
        try
        {
            TextWriter tw = new StreamWriter(fileName,false);
            tw.WriteLine("Name,Password,Score,NumberOfDeath,TimePlaying,Level1,Level2,Level3,Level4,Level5,Level6");
            foreach (var acc in accounts)
            {
                if (Menu.selectedLevel != null)
                {
                    switch (Menu.selectedLevel)
                    {
                        case "level1"://LEVEL 1
                            if (timeCounting < acc.Level1)
                            {
                                acc.Level1 = timeCounting;
                            }
                            break;
                        case "level2"://LEVEL 2
                            if (timeCounting < acc.Level2)
                            {
                                acc.Level2 = timeCounting;
                            }
                            break;
                        case "level3"://LEVEL 3
                            if (timeCounting < acc.Level3)
                            {
                                acc.Level3 = timeCounting;
                            }
                            break;
                        case "level4"://LEVEL 4
                            if (timeCounting < acc.Level4)
                            {
                                acc.Level4 = timeCounting;
                            }
                            break;
                        case "level5"://LEVEL 5
                            if (timeCounting < acc.Level5)
                            {
                                acc.Level5 = timeCounting;
                            }
                            break;
                        case "level6"://LEVEL 6
                            if (timeCounting < acc.Level6)
                            {
                                acc.Level6 = timeCounting;
                            }
                            break;
                    }
                }

                tw.WriteLine(acc.Name + "," + acc.Password + ","
                    + acc.Score + "," + acc.NumberOfDeath + ","
                    + acc.TimePLaying + "," + acc.Level1 + ","
                    + acc.Level2 + "," + acc.Level3 + ","
                    + acc.Level4 + "," + acc.Level5 + ","
                    + acc.Level6);
            }
            tw.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
