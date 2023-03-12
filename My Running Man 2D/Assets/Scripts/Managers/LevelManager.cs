using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static Action<HeroMotor> OnPlayerSpawn;

    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefab;

    [Header("Levels")]
    [SerializeField] private GameObject[] levels;

    private HeroMotor _currentPlayer;

    private void Awake()
    {
        SpawnPlayer(playerPrefab);
        UIPlayer.ResetTimeCounting();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RevivePlayer();
        }
    }

    private void Start()
    {
        //Call Event when Spawn
        OnPlayerSpawn?.Invoke(_currentPlayer);
    }

    private void InitLevel(int levelIndex)
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
        levels[levelIndex].SetActive(true);
    }


    private void SpawnPlayer(GameObject player)
    {
        if (player != null)
        {
            _currentPlayer = Instantiate(player, levelStartPoint.position, Quaternion.identity).GetComponent<HeroMotor>();
            _currentPlayer.GetComponent<Health>().ResetLife();
        }
    }

    //Hoi sinh nhan vat
    private void RevivePlayer()
    {
        if (_currentPlayer != null)
        {
            if (SceneManager.GetActiveScene().name == "SpecialRound")
            {
                Menu.selectedLevel = "special";
                SceneManager.LoadScene("GameOver");
            }
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint); //chuyen nhan vat den vi tri moi
            _currentPlayer.GetComponent<Health>().ResetLife(); //reset lai UI heart tren man hinh
            _currentPlayer.GetComponent<Health>().Revive();
        }
    }

    private void PlayerDeath(HeroMotor player)
    {
        _currentPlayer.gameObject.SetActive(false);
        RevivePlayer();
    }

    private void OnEnable()
    {
        Health.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= PlayerDeath;
    }
}
