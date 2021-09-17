using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefsSetter : MonoBehaviour
{
    private void Awake()
    {
        int countLaunches = PlayerPrefs.GetInt(Strings.PlayerPrefs.CountLaunches, 0);

        countLaunches++;

        if (countLaunches == 1)
        {
            PlayerPrefs.SetInt(Strings.PlayerPrefs.Wallet, 0);
            PlayerPrefs.SetString(Strings.Algorithms.CenterAlg, Strings.AlgorithmsParameters.CenterAlg.CenterAlgDefault);
            PlayerPrefs.SetString(Strings.PlayerPrefs.Ship, Strings.Ships.SmallFighter);
            PlayerPrefs.SetString(Strings.PlayerPrefs.ShipDefault, Strings.Ships.SmallFighter);
            PlayerPrefs.SetString(Strings.PlayerPrefs.Mode, Strings.Modes.ClassicMode);
            PlayerPrefs.SetInt(Strings.PlayerPrefs.CountLaunches, countLaunches);
            PlayerPrefs.Save();
        }
    }
}
