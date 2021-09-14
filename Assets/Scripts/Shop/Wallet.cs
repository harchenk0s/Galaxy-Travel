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
            PlayerPrefs.SetInt("Wallet", GetWallet() - price);
            PlayerPrefs.Save();
            WalletChange.Invoke(GetWallet());
            return true;
        }
        else
        {
            return false;
        }
    }
    //TODO: DELETE UPDATE
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Add(100000);
        }
    }
    public void Add(int value)
    {
        int wallet = PlayerPrefs.GetInt("Wallet", 0);
        PlayerPrefs.SetInt("Wallet", Mathf.Clamp(wallet + value, 0, 999999));
        PlayerPrefs.Save();
        WalletChange.Invoke(GetWallet());
    }

    public int GetWallet()
    {
        return PlayerPrefs.GetInt("Wallet", 0);
    }
}
