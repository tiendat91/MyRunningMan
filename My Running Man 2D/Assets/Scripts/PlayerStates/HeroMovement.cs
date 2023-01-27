using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    private float _horizontalMovement;
    private float _movement;
    public override void ExecuteState()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //di chuyen
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            _movement = _horizontalMovement;
        }
        else
        {
            _movement = 0f;
        }

        float moveSpeed = _movement * speed;
        _playerController.SetHorizontalForce(moveSpeed);
    }

    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
    }
}
