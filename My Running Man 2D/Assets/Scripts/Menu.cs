using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
    [SerializeField] public GameObject[] panels;
    [SerializeField] public Button[] buttons;
    [SerializeField] public AudioSource _audio;
    [SerializeField] public TextMeshProUGUI SnailTalk;
    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private TextMeshProUGUI _scoreBoard;
    [SerializeField] private TextMeshProUGUI _timeBoard;

    public static List<Account> accounts = new List<Account>();
    private Account _currentAccount;
    private static List<Account> top5HighScore = new List<Account>();
    private static string _currentName;
    private static string _currentPassword;
    private static string _currentConfirmPassword;
    private string fileName = null;
    public static int selectedLevel;
    public string NameLogin { get; set; }
    private string KEY_NAME = "MyGame_NAME";
    private static string KEY_PROCESS = "MyGame_PROCESS";

    public List<Account> Accounts { get; set; }

    void Start()
    {
        LoadGameProcess();

        Time.timeScale = 1f;
        //ReadData();
        //GetHighScore();
        fileName = Application.dataPath + "/Data/Account_Player.csv";
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        panels[0].SetActive(true);
    }

    void Update()
    {
    }

    public void LoadGameProcess()
    {
        string savedJson = PlayerPrefs.GetString(KEY_PROCESS);

        //deactive all level
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        //open first-level
        int currentLevel = 1;
        if (!string.IsNullOrEmpty(savedJson))
        {
            GameProgress loadedProgress = JsonUtility.FromJson<GameProgress>(savedJson);
            if(loadedProgress.level !=0 && loadedProgress.level != null)
            {
                currentLevel = (int)loadedProgress.level;
            }
            // Use the loadedProgress data in your game
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i <= (currentLevel-1))
                {
                    buttons[i].interactable = true;
                }
            }
        }
        else
        {
            //Store game for new player
            SaveGameProcess(1, 0);
        }
        buttons[currentLevel-1].interactable = true;
    }
    public static void SaveGameProcess(int? level, int? score)
    {
        GameProgress gameProgress = new GameProgress();
        gameProgress.level = (int)level;
        gameProgress.score = (int)score;
        string json = JsonUtility.ToJson(gameProgress);
        PlayerPrefs.SetString(KEY_PROCESS, json);
        PlayerPrefs.Save();
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
        ReadData();

        ChangeSnailTaking("READY FOR YOUR ADVENTURE?");
    }
    public void QuitGame()
    {
        ReadData();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void TurnOnOffAudio(bool isCheck)
    {
        _audio.GetComponent<AudioSource>().mute = isCheck;
    }
    public void TypingName(string n)
    {
        _currentName = n.ToLower().Trim();
        Debug.Log(_currentName);

    }
    public void TypingPassword(string p)
    {
        _currentPassword = p.ToLower().Trim();
        Debug.Log(_currentPassword);
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
    public void LogInAccount(GameObject levelMenu)
    {
        foreach(var acc in accounts)
        {
            if(acc.Name ==  _currentName && acc.Password == _currentPassword)
            {
                _currentAccount = acc;
                ChangeSnailTaking("LOGIN SUCCESS!");
                ChangetoOtherMenu(levelMenu);
                //DisplayLevel();
                PlayerPrefs.SetString(KEY_NAME, acc.Name);
                return;
            }
        }
        ChangeSnailTaking("LOGIN FAIL!");

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
        for (int i = NumberOfPropertiesInData; i < data.Length - NumberOfPropertiesInData; i += NumberOfPropertiesInData)
        {
            accounts.Add(new Account()
            {
                Name = data[i],
                Password = data[i + 1],
                Score = Int32.Parse(data[i + 2]),
                NumberOfDeath = Int32.Parse(data[i + 3]),
                Level1 = float.Parse(data[i + 5]),
                Level2 = float.Parse(data[i + 6]),
                Level3 = float.Parse(data[i + 7]),
                Level4 = float.Parse(data[i + 8]),
                Level5 = float.Parse(data[i + 9]),
                Level6 = float.Parse(data[i + 10]),

                TimePLaying = float.Parse(data[i + 5]) 
                + float.Parse(data[i + 6]) 
                + float.Parse(data[i + 7]) 
                + float.Parse(data[i + 8]) 
                + float.Parse(data[i + 9]) 
                + float.Parse(data[i + 10])

            });
        }

        Accounts = accounts;
    }
    public void GetHighScore()
    {
        //TOP 5 NGUOI CHOI: HOAN THANG 6 MAN TRONG THOI GIAN NHANH NHAT
        top5HighScore = accounts.AsQueryable().Where(x => x.Level6 > 0).OrderBy(x => x.TimePLaying).ToList();

        int count = 0;
        _scoreBoard.text = "";
        _timeBoard.text = "";
        if(top5HighScore.Count == 0)
        {
            _scoreBoard.text = "NO DATA";
            return;
        }
        foreach (var item in top5HighScore)
        {
            _scoreBoard.text += $"Top {++count}:" + item.Name + "\n";
            _timeBoard.text += TimeSpan.FromSeconds(item.TimePLaying).ToString("hh':'mm':'ss") + "\n";
        }

    }
    public void WriteData()
    {
        try
        {
            TextWriter tw = new StreamWriter(this.fileName,false);
            tw.WriteLine("Name,Password,Score,NumberOfDeath,TimePlaying,Level1,Level2,Level3,Level4,Level5,Level6");
            foreach (var acc in accounts)
            {
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
    public void RegisterAccount(GameObject loginMenu)
    {
        if(_currentName == null)
        {
            return;
        }
        if (_currentPassword == null)
        {
            return;
        }
        if (_currentConfirmPassword == null)
        {
            return;
        }
        if (_currentConfirmPassword != _currentPassword)
        {
            return;
        }

        //KIEM TRA TRUNG TEN
        foreach (var acc in accounts)
        {
            if(acc.Name == _currentName)
            {
                ChangeSnailTaking("DUPLICATE NAME!");
                return;
            }
        }

        accounts.Add(new Account()
        {
            Name = _currentName,
            Password = _currentPassword,
            Score = 0,
            NumberOfDeath = 0,
            TimePLaying = 0,
        });

        WriteData();
        ChangeSnailTaking("REGISTER SUCCESS!");
        ChangetoOtherMenu(loginMenu);
    }
    public void ChoosingLevel(int level)
    {
        selectedLevel = level;    
        SceneManager.LoadScene("Loading");
    }
}
