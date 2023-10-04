using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<HeroMotor> OnDeath;
    public static Action<HeroMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 3;

    public int MaxLifes => _maxLifes;

    public int CurrentLifes => _currentLifes;

    private int _maxLifes;
    private int _currentLifes;

    private void Awake()
    {
        _maxLifes = lifes;
    }

    private void Start()
    {
        ResetLife();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        _currentLifes = 0;
        SoundManagers.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        UpdateLifesUI();
        OnDeath?.Invoke(gameObject.GetComponent<HeroMotor>());
    }

    public void ResetLife()
    {

        _currentLifes = lifes;
        UpdateLifesUI();
    }

    public void Revive()
    {
        OnRevive?.Invoke(gameObject.GetComponent<HeroMotor>());
    }

    private void UpdateLifesUI()
    {
        OnLifesChanged?.Invoke(_currentLifes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<HeroMotor>());
        }
    }
}
