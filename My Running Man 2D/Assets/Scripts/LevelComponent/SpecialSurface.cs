using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float friction = 0.1f; //luc ma sat

    [Header("Movement")]
    [SerializeField] private float horizontalMovement = 4f;
    public float Friction => friction;

    private HeroController _playerController;

    private void Update()
    {
        if (_playerController == null)
        {
            return;
        }
        _playerController.AddHorizontalMovement(horizontalMovement);
    }

    //Speed up hero when trigger to object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HeroController>() != null)
        {
            _playerController = collision.gameObject.GetComponent<HeroController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerController = null;
    }
}
