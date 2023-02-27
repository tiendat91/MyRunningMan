using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
    [SerializeField] public GameObject[] panels;
    [SerializeField] public AudioSource _audio;
    [SerializeField] public TextMeshProUGUI SnailTalk;
    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private TextMeshProUGUI _scoreBoard;

    private List<Account> accounts = new List<Account>();
    private List<Account> top10HighScore = new List<Account>();
    private string _currentName;
    private string _currentPassword;
    private string _currentConfirmPassword;


    void Start()
    {
        ReadData();
        GetHighScore();

        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangetoOtherMenu(GameObject gameObject)
    {
        foreach (var panel in panels)
        {
            if (panel == gameObject)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    public void BackToMainMenu()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);

        //change snail's text
        ChangeSnailTaking("READY FOR YOUR ADVANTURE?");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void TurnOnOffAudio(bool isCheck)
    {
        _audio.GetComponent<AudioSource>().mute = isCheck;
    }

    public void TypingName(string n)
    {
        _currentName = n.ToLower().Trim();
    }
    public void TypingPassword(string p)
    {
        _currentPassword = p.ToLower().Trim();
    }
    public void TypingConfirmPassword(string cf)
    {
        _currentConfirmPassword = cf.ToLower().Trim();
        if (_currentConfirmPassword != _currentPassword)
        {
            ChangeSnailTaking("CONFIRM PASSWORD NOT MATCH");
        }
        else
        {
            ChangeSnailTaking("CONFIRM PASSWORD IS MATCH");
        }
    }

    public void LogInAccount()
    {
        Debug.Log(_currentName + " " + _currentPassword);
        ChangeSnailTaking("NOT FOUND ACCOUNT");
    }

    public void ChangeSnailTaking(string s)
    {
        SnailTalk.text = s;
    }

    public void LinkContact(int options)
    {
        switch (options)
        {
            case 0:
                Application.OpenURL("https://www.facebook.com/datdat910");
                break;
            case 1:
                Application.OpenURL("https://tiendat91.itch.io");
                break;
        }
    }

    public void ReadData()
    {
        string[] data = _textAsset.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        int NumberOfPropertiesInData = typeof(Account).GetProperties().Length;
        int tableSize = data.Length / NumberOfPropertiesInData - 1;
        for (int i = 0; i < tableSize; i++)
        {
            accounts.Add(new Account()
            {
                Name = data[NumberOfPropertiesInData],
                Password = data[NumberOfPropertiesInData + 1],
                Score = Int32.Parse(data[NumberOfPropertiesInData + 2]),
                NumberOfDeath = Int32.Parse(data[NumberOfPropertiesInData + 3]),
                TimePLaying = float.Parse(data[NumberOfPropertiesInData + 4]),
            });
        }
    }

    public void GetHighScore()
    {
        top10HighScore.AddRange(accounts.AsQueryable().OrderBy(s => s.TimePLaying).Take(10).ToList());
        int count = 0;
        _scoreBoard.text = "";
        foreach (var item in top10HighScore)
        {
            _scoreBoard.text += $"Top {++count}:" + item.Name + ", " + item.TimePLaying + "\n";
        }
    }
}
