using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Text _textField;

    private Wallet _wallet;

    public void ReciveWalletValue(int value)
    {
        _textField.text = value.ToString();
    }

    private void Awake()
    {
        _wallet = FindObjectOfType<Wallet>();
    }

    private void Start()
    {
        ReciveWalletValue(_wallet.GetWallet());
    }
}
