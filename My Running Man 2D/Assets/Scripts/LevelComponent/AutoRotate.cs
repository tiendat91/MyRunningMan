using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float speed = 4f;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(rotateAxis * speed * Time.deltaTime);
    }
}
