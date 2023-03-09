using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    private float _horizontalMovement;
    private float _movement;
    public float Speed { get; set; }
    public float InitialSpeed => speed;
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

        //Calculate character's speed when collide with special surfaces
        moveSpeed = EvaluateFriction(moveSpeed);

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

    //Calculate character's speed when collide with special surfaces
    private float EvaluateFriction(float moveSpeed)
    {
        if (_playerController.Friction > 0)
        {
            moveSpeed = Mathf.Lerp(_playerController.Force.x, moveSpeed
                , Time.deltaTime * 10f * _playerController.Friction);
        }

        return moveSpeed;
    }
}
