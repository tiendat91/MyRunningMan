using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartingPlay()
    {
        switch (Menu.selectedLevel)
        {
            case "special"://SPECIAL ROUND
                SceneManager.LoadScene("SpecialRound");
                break;
            case "level1"://LEVEL 1
                SceneManager.LoadScene("Level1");
                break;
            case "level2"://LEVEL 2
                SceneManager.LoadScene("Level2");
                break;
            case "level3"://LEVEL 3
                SceneManager.LoadScene("Level3");
                break;
            case "level4"://LEVEL 4
                SceneManager.LoadScene("Level4");
                break;
            case "level5"://LEVEL 5
                SceneManager.LoadScene("Level5");
                break;
            case "level6"://LEVEL 6
                SceneManager.LoadScene("Level6");
                break;
            case "menu":
                SceneManager.LoadScene("Menu");
                break;
        }
    }
}
