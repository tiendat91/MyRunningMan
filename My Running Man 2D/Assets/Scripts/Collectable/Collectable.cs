using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected HeroMotor _playerMotor;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void CollectLogic()
    {
        if (!CanBePicked())
        {
            return;
        }
       // SoundManagers.Instance.PlaySound(AudioLibrary.Instance.CollectableClip);
        Collect();
        DisableCollectalbe();
    }

    protected virtual void Collect()
    {
        Debug.Log("Item collected");
    }

    private void DisableCollectalbe()
    {
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
    }
    private bool CanBePicked()
    {
        return _playerMotor != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<HeroMotor>() != null)
        {
            _playerMotor = collision.GetComponent<HeroMotor>();
            CollectLogic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerMotor = null;
    }
}
