using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefsSetter : MonoBehaviour
{
    public int counts;

    void Start()
    {
        if (PlayerPrefs.HasKey("CountLaunches"))
        {
            int countLaunches = PlayerPrefs.GetInt("CountLaunches") + 1;
            PlayerPrefs.SetInt("CountLaunches", countLaunches);
            counts = PlayerPrefs.GetInt("CountLaunches");
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("CountLaunches", 1);
            counts = PlayerPrefs.GetInt("CountLaunches");
            PlayerPrefs.Save();
        }
    }
}
