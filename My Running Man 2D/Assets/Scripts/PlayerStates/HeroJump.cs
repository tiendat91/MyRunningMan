using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJump : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

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
}
