using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStates : MonoBehaviour
{
    protected HeroController _playerController;
    protected Animator _animator;
    protected float _horizontalInput;
    protected float _verticalInput;
    private float buttonClick;
    protected virtual void Start()
    {
        InitState();
    }

    protected virtual void InitState()
    {
        _playerController = GetComponent<HeroController>();
        _animator = GetComponent<Animator>();
    }
    public virtual void ExecuteState()
    {

    }

    public void GoLeft()
    {
        buttonClick = -1;
        GetInput();
    }
    public void GoRight()
    {
        buttonClick = 1;
        GetInput();
    }
    public void UnpressButton()
    {
        buttonClick = 0;
        GetInput();
    }


    public virtual void LocalInput()
    {
        _horizontalInput = buttonClick;
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        GetInput();
    }

    protected virtual void GetInput()
    {

    }

    public virtual void SetAnimation()
    {

    }

}
