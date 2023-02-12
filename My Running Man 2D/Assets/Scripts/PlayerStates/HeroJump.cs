using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJump : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    private int _jumpAnimationParameter = Animator.StringToHash("Jump");
    private int _doubleJumpParameter = Animator.StringToHash("DoubleJump");
    private int _fallAnimatorParameter = Animator.StringToHash("Fall");
    public int JumpLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpLeft = maxJumps;
    }
    public override void ExecuteState() //call each frame when we want to know if we are on the ground rightnow
    {
        if (_playerController.Conditions.IsCollidingBelow && _playerController.Force.y == 0f) //standing in the ground
        {
            JumpLeft = maxJumps; //reset number of jump consecutively
            _playerController.Conditions.IsJumping = false;
        }
    }
    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!CanJump())
        {
            return;
        }
        if (JumpLeft == 0)
        {
            return;
        }

        JumpLeft -= 1;
        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(_playerController.Gravity));
        _playerController.SetVerticalForce(jumpForce);
        _playerController.Conditions.IsJumping = true;
    }

    private bool CanJump()
    {
        if (!_playerController.Conditions.IsCollidingBelow && JumpLeft <= 0)
        {
            return false;
        }
        if (_playerController.Conditions.IsCollidingBelow && JumpLeft <= 0)
        {
            return false;
        }
        return true;
    }

    public override void SetAnimation()
    {
        //Jump
        _animator.SetBool(_jumpAnimationParameter, _playerController.Conditions.IsJumping
                && !_playerController.Conditions.IsCollidingBelow
                && JumpLeft > 0
                && !_playerController.Conditions.IsFalling
                && !_playerController.Conditions.IsJetPacking);

        //Double Jump
        _animator.SetBool(_doubleJumpParameter, _playerController.Conditions.IsJumping
                && !_playerController.Conditions.IsCollidingBelow
                && JumpLeft == 0
                && !_playerController.Conditions.IsFalling
                && !_playerController.Conditions.IsJetPacking);

        //Fall
        _animator.SetBool(_fallAnimatorParameter, _playerController.Conditions.IsFalling
                && _playerController.Conditions.IsJumping
                && !_playerController.Conditions.IsCollidingBelow
                && !_playerController.Conditions.IsJetPacking);
    }
}
