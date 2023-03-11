using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static Action<int> OnLevelCompleted;

    [Header("Settigns")]
    [SerializeField] private string nextLevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Menu.selectedLevel = nextLevelName;
            SceneManager.LoadScene("Loading");
        }
    }

}
