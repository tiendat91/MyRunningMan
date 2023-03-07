using System.Collections;
using System.Collections.Generic;
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
            case 0://SPECIAL ROUND
                SceneManager.LoadScene(2);
                break;
            case 1://LEVEL 1
                SceneManager.LoadScene(2);
                break;
            case 2://LEVEL 2
                SceneManager.LoadScene(2);
                break;
            case 3://LEVEL 3
                SceneManager.LoadScene(2);
                break;
            case 4://LEVEL 4
                SceneManager.LoadScene(2);
                break;
            case 5://LEVEL 5
                SceneManager.LoadScene(2);
                break;
            case 6://LEVEL 6
                SceneManager.LoadScene(2);
                break;
        }
    }
}
