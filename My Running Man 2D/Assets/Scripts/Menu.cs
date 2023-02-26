using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
    [SerializeField] public GameObject[] panels;
    [SerializeField] public AudioSource _audio;
    [SerializeField] public TextMeshProUGUI SnailTalk;

    private Account _account;
    private string _currentName;
    private string _currentPassword;
    private string _currentConfirmPassword;

    void Start()
    {
        _account = GetComponent<Account>();

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

    public void LinkContact()
    {
        Application.OpenURL("https://www.facebook.com/datdat910");
    }
}
