using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISpecialRound : MonoBehaviour
{
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject optionsSettings;
    [SerializeField] private TextMeshProUGUI timeCount;
    [SerializeField] private TextMeshProUGUI enemyKill;
    [SerializeField] public AudioSource _audio;

    [SerializeField] private float timeCounting = 10;
    private float bonusTimeByEnemy = 5;
    [SerializeField] private bool timerIsRunning = false;
    private int enemyCount = 0;
    private int enemyKillCount = 0;
    void Start()
    {
        timerIsRunning = true;
        panelSettings.SetActive(false);
        optionsSettings.SetActive(false);
        GameObject[] enemies = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer("Enemy")).ToArray();
        enemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCountUpdate = 0;
        GameObject[] enemies = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer("Enemy")).ToArray();
        enemyCountUpdate = enemies.Length;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TurnOnPauseMenu(true);
        }

        if (timerIsRunning)
        {
            if (timeCounting > 0)
            {
                timeCounting -= Time.deltaTime;
                if (enemyCountUpdate < enemyCount)
                {
                    timeCounting += bonusTimeByEnemy;
                    enemyKillCount += 1;
                    enemyKill.text = "0" + enemyKillCount;
                    enemyCount = enemyCountUpdate;
                }

            }
            else
            {
                Debug.Log("Time has run out!");
                Debug.Log("GameOver");
                SceneManager.LoadSceneAsync("Menu");
                timeCounting = 0;
                timerIsRunning = false;
            }
        }
        if (enemyCountUpdate == 0)
        {
            SceneManager.LoadSceneAsync("Menu");
            timeCounting = 0;
            timerIsRunning = false;
        }
        if (GameObject.Find("Player(Clone)") == null)
        {
            SceneManager.LoadSceneAsync("Menu");
            timeCounting = 0;
            timerIsRunning = false;
        }
        timeCount.text = TimeSpan.FromSeconds(timeCounting).ToString("hh':'mm':'ss");
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

}
