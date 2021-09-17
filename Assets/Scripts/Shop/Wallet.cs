using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public IntEvent WalletChange;

    public bool Buy(int price)
    {
        if(price <= GetWallet())
        {
            PlayerPrefs.SetInt(Strings.PlayerPrefs.Wallet, GetWallet() - price);
            PlayerPrefs.Save();
            WalletChange.Invoke(GetWallet());
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Add(int value)
    {
        int wallet = PlayerPrefs.GetInt(Strings.PlayerPrefs.Wallet, 0);
        PlayerPrefs.SetInt(Strings.PlayerPrefs.Wallet, Mathf.Clamp(wallet + value, 0, 999999));
        PlayerPrefs.Save();
        WalletChange.Invoke(GetWallet());
    }

    public int GetWallet()
    {
        return PlayerPrefs.GetInt(Strings.PlayerPrefs.Wallet, 0);
    }
}
