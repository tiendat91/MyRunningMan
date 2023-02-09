
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : Singleton<UIManagers>
{
    [Header("Settings")]
    [SerializeField] private Image fuelImage;

    private float _currentJetpackFuel;
    private float _jetpackFuel;


    // Start is called before the first frame update
    void Update()
    {
        ChangeValueJetPackage();
    }

    // Update is called once per frame
    public void UpdateFuel(float currentFuel, float maxFuel)
    {
        _currentJetpackFuel = currentFuel;
        _jetpackFuel = maxFuel;
    }

    private void ChangeValueJetPackage()
    {
        fuelImage.fillAmount = Mathf.Lerp(fuelImage.fillAmount, _currentJetpackFuel / _jetpackFuel, Time.deltaTime * 10f);
    }
}
