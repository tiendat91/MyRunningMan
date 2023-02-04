using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCling : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float fallFactor = 0.5f;
    protected override void GetInput()
    {
        if (_horizontalInput <= -0.1f || _horizontalInput >= 0.1f)
        {
            WallCling();
        }
    }
    public override void ExecuteState()
    {
        ExitWallCling();
    }
    private void WallCling()
    {
        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
        {
            return;
        }

        if (_playerController.Conditions.IsCollidingLeft && _horizontalInput <= -0.1f ||
            _playerController.Conditions.IsCollidingRight && _horizontalInput >= 0.1f)
        {
            _playerController.SeteWallClingMultiplier(fallFactor);
            _playerController.Conditions.IsWallCling = true;
        }
    }

    private void ExitWallCling()
    {
        if (_playerController.Conditions.IsWallCling)
        {
            if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
            {
                _playerController.SeteWallClingMultiplier(0f);
                _playerController.Conditions.IsWallCling = false;
            }

            if (_playerController.FacingRight)
            {
                if (_horizontalInput <= -0.1f || _horizontalInput < 0.1f) //treo phia ben phai, nhan quay sang trai -> roi
                {
                    _playerController.SeteWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallCling = false;
                }
            }
            else
            {
                if (_horizontalInput > -0.1f || _horizontalInput > -0.1f)//treo phia ben trai, nhan quay sang phai -> roi
                {
                    _playerController.SeteWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallCling = false;
                }
            }
        }
    }
}
