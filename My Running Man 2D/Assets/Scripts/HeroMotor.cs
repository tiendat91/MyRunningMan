using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMotor : MonoBehaviour
{
    private HeroStates[] _playerStates;

    // Start is called before the first frame update
    void Start()
    {
        _playerStates = GetComponents<HeroStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStates.Length != 0)
        {
            foreach (HeroStates playerState in _playerStates)
            {
                playerState.LocalInput();
                playerState.ExecuteState();
            }
        }
    }
}
