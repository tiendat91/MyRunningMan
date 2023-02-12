using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class HeroJetpack : HeroStates
{
    [Header("Settings")]
    [SerializeField] private float jetpackForce = 3f;
    [SerializeField] private float jetpackFuel = 5f;



    public float _fuelLeft;
    private float _fuelDurationLeft;
    private bool _stillHaveFuel = true;

    private int _jetpackParameter = Animator.StringToHash("Jetpack");

    protected override void InitState()
    {
        base.InitState();
        _fuelDurationLeft = jetpackFuel;
        _fuelLeft = jetpackFuel;
        UIManagers.Instance.UpdateFuel(_fuelLeft, jetpackFuel); //SINGELETON PATTERN
    }
    protected override void GetInput()
    {
        if (Input.GetKey(KeyCode.X))
        {
            JetPack();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            EndJetPack();
        }
    }
    private void Update()
    {
    }

    private void JetPack()
    {
        if (!_stillHaveFuel)
        {
            return;
        }
        if (_fuelLeft <= 0)
        {
            EndJetPack();
            _stillHaveFuel = false;
            return;
        }
        _playerController.SetVerticalForce(jetpackForce);
        _playerController.Conditions.IsJetPacking = true;
        StartCoroutine(BurnFuel());
    }

    private void EndJetPack()
    {
        _playerController.Conditions.IsJetPacking = false;
        StartCoroutine(Refill());
    }

    private IEnumerator BurnFuel()
    {
        float fuelConsumed = _fuelLeft;
        if (fuelConsumed > 0 && _playerController.Conditions.IsJetPacking && _fuelLeft <= fuelConsumed)
        {
            fuelConsumed -= Time.deltaTime;
            _fuelLeft = fuelConsumed;

            UIManagers.Instance.UpdateFuel(_fuelLeft, jetpackFuel); //SINGELETON PATTERN

            yield return null;
        }
    }

    private IEnumerator Refill()
    {
        yield return new WaitForSeconds(0.5f);
        float fuel = _fuelLeft;
        while (fuel < jetpackFuel && !_playerController.Conditions.IsJetPacking)
        {
            fuel += Time.deltaTime;
            _fuelLeft = fuel;

            UIManagers.Instance.UpdateFuel(_fuelLeft, jetpackFuel); //SINGELETON PATTERN


            if (!_stillHaveFuel && fuel > 0.2f)
            {
                _stillHaveFuel = true;
            }
            yield return null;
        }
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_jetpackParameter, _playerController.Conditions.IsJetPacking);
    }
}
