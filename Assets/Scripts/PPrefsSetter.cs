using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefsSetter : MonoBehaviour
{
    private void Awake()
    {
        int countLaunches = PlayerPrefs.GetInt("CountLaunches", 0);

        if (countLaunches > 0)
        {
            countLaunches++;
            PlayerPrefs.SetInt("CountLaunches", countLaunches);
            PlayerPrefs.SetString("GridRandomAlg", "OnlyAsteroids");
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("CountLaunches", 1);
            PlayerPrefs.SetString("GridRandomAlg", "OnlyCows");
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
