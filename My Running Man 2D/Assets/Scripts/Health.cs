using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<HeroMotor> OnDeath;
    public static Action<HeroMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 3;

    private int _maxlifes;
    private int _currentlifes;

    private void Awake()
    {
        _maxlifes = lifes;
    }

    private void Start()
    {
        ResetLife();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }
    }

    private void AddLife()
    {
        _currentlifes += 1;
        if (_currentlifes > _maxlifes)
        {
            _currentlifes = _maxlifes;
        }
        UpdatelifesUI();

    }

    public void LoseLife()
    {
        _currentlifes -= 1;
        if (_currentlifes <= 0)
        {
            _currentlifes = 0;
            OnDeath?.Invoke(gameObject.GetComponent<HeroMotor>());
        }
        UpdatelifesUI();
    }

    public void ResetLife()
    {
        _currentlifes = lifes;
        UpdatelifesUI();
    }

    private void UpdatelifesUI()
    {
        OnLifesChanged?.Invoke(_currentlifes);
    }

    ///////////////////////////////////
    public void KillPlayer()
    {
        _currentlifes = 0;
        UpdatelifesUI();
        OnDeath?.Invoke(gameObject.GetComponent<HeroMotor>());
    }

    ///////////////////////////////////////////

    public void Revive()
    {
        OnRevive?.Invoke(gameObject.GetComponent<HeroMotor>());
    }
}