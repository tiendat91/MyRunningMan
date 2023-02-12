using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefab;

    private HeroMotor _currentPlayer;

    private void Awake()
    {
        SpawnPlayer(playerPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RevivePlayer();
        }
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
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint); //chuyen nhan vat den vi tri moi
            _currentPlayer.GetComponent<Health>().ResetLife(); //reset lai UI heart tren man hinh
        }
    }

    private void PlayerDeath(HeroMotor player)
    {
        _currentPlayer.gameObject.SetActive(false);
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