using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static Action<int> OnLevelCompleted;
    private string KEY_KEYS = "MyGame_TOTAL_KEYS";


    [Header("Settigns")]
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject chatBubble;

    private void Start()
    {
        chatBubble.active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt(KEY_KEYS, 0) == 3) {
                //UIPlayer.WriteDataWithTimeIntoAccount();
                Menu.selectedLevel = nextLevelName;
                SceneManager.LoadScene("Loading");
            }
            else
            {
                chatBubble.active = true;
            }
        }
    }

}
