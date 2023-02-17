using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float shakeVibration = 10f;
    [SerializeField] private float shakeRandomness = 0.2f;
    [SerializeField] private float shakeTime = 0.01f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shake();
        }
    }
    public void Shake()
    {
        StartCoroutine(IEShake());
    }
    private IEnumerator IEShake()
    {
        Vector3 currentPos = transform.position;
        for (int i = 0; i < shakeVibration; i++)
        {
            // set random position = current position + random position (with radius 1.0)
            Vector3 shakePos = currentPos + Random.onUnitSphere * shakeRandomness;
            yield return new WaitForSeconds(shakeTime);
            transform.position = shakePos;
        }
    }
}
