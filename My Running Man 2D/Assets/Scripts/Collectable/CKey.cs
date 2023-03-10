using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKey : Collectable
{
    [Header("Settings")]
    [SerializeField] private int amountToAdd = 1;

    protected override void Collect()
    {
        AddKeys();
    }
    private void AddKeys()
    {
        KeyManager.Instance.AddKeys(amountToAdd);
    }

}
