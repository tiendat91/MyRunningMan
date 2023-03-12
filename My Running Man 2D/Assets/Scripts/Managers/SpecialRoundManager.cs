using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialRoundManager : MonoBehaviour
{
    [SerializeField]public static int NumberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        NumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Count();
    }

    //public static void ReduceNumberEnemy()
    //{
    //    NumberOfEnemies -= 1;
    //    Debug.Log(NumberOfEnemies);
    //}

    // Update is called once per frame
    void Update()
    {
        NumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Count();
        if (NumberOfEnemies == 0)
        {
            Menu.selectedLevel = "special";
            SceneManager.LoadScene("GameOver");
        }
        
    }
}
