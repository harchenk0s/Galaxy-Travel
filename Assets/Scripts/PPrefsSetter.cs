using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefsSetter : MonoBehaviour
{
    private void Awake()
    {
        int countLaunches = PlayerPrefs.GetInt("CountLaunches", 0);

        countLaunches++;

        if (countLaunches == 1)
        {
            PlayerPrefs.SetString("CenterAlg", "CenterAlgDefault");
            PlayerPrefs.SetString("Ship", "SmallFighter");
            PlayerPrefs.SetString("Mode", "Classic");
            PlayerPrefs.SetInt("CountLaunches", countLaunches);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        //TODO Delete this!
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
