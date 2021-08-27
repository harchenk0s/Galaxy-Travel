using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefsSetter : MonoBehaviour
{
    private void Awake()
    {
        int countLaunches = PlayerPrefs.GetInt("FirstLaunch", 1);

        if (countLaunches != 1)
        {
            countLaunches++;
            PlayerPrefs.SetInt("CountLaunches", countLaunches);
            PlayerPrefs.SetString("GridRandomAlg", "GridRandomAlgDefault");
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
