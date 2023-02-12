using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    private float _horizontalMovement;
    private float _movement;

    private int _idleAnimatorParameter = Animator.StringToHash("Idle");
    private int _runAnimatorParameter = Animator.StringToHash("Run");

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

    public override void SetAnimation()
    {
        //call when on the ground and not moving
        _animator.SetBool(_idleAnimatorParameter, _horizontalMovement == 0 && _playerController.Conditions.IsCollidingBelow);
        //call when pressing left/right key and moving
        _animator.SetBool(_runAnimatorParameter, Mathf.Abs(_horizontalInput) > 0.1f && _playerController.Conditions.IsCollidingBelow);
    }
}
