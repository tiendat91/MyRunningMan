using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class CheckPoint : MonoBehaviour
{
    public static Action<int> OnLevelCompleted;
    private string KEY_KEYS = "MyGame_TOTAL_KEYS";


    [Header("Settings")]
    [SerializeField] private int nextLevel;
    [SerializeField] private GameObject chatBubble;

    private void Start()
    {
        chatBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt(KEY_KEYS, 0) == 3) {
                Menu.SaveGameProcess(nextLevel, 0);
                Menu.selectedLevel = nextLevel;
                SceneManager.LoadScene("Loading");
            }
            else
            {
                chatBubble.SetActive(true);
            }
        }
    }
}
