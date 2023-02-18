
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : Singleton<UIManagers>
{
    [Header("Settings")]
    [SerializeField] private Image fuelImage;
    [SerializeField] private GameObject[] playerLifes;

    private float _currentJetpackFuel;
    private float _jetpackFuel;

    //private Stack<Color> colorStack = new Stack<Color>();
    //private SpriteRenderer renderer;

    //private void Start()
    //{
    //    renderer = GetComponent<SpriteRenderer>();
    //    colorStack.Push(renderer.color);
    //}

    //public void UseMana()
    //{
    //    Color newColor = renderer.color;
    //    newColor.r -= 0.1f;
    //    newColor.g -= 0.1f;
    //    renderer.color = newColor;
    //    colorStack.Push(newColor);
    //}

    //public void GainMana()
    //{
    //    if (colorStack.Count > 0)
    //    {
    //        renderer.color = colorStack.Pop();
    //    }
    //}


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

    private void OnPlayerLifes(int currentLifes)
    {
        for (int i = 0; i < playerLifes.Length; i++)
        {
            if (i < currentLifes) //ex 2 
            {
                playerLifes[i].gameObject.SetActive(true);
            }
            else
            {
                playerLifes[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        Health.OnLifesChanged += OnPlayerLifes;
    }

    private void OnDisable()
    {
        Health.OnLifesChanged -= OnPlayerLifes;
    }
}
