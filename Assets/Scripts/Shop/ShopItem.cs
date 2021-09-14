using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new item", menuName = "ShopItems", order = 51)]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _pictureShadow;
    [SerializeField] private Sprite _picture;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isBuyed = false;

    private LevelBuilder _levelBuilder;

    public UnityEvent BuyEvent;

    public bool IsBuyed
    {
        get { return _isBuyed; }
        private set
        {
            _isBuyed = value;
            BuyEvent.Invoke();
        }
    }

    public string Name => _name;
    public int Price => _price;
    public GameObject Prefab => _prefab;

    public Sprite Picture
    {
        get
        {
            if (_isBuyed)
                return _picture;
            else
                return _pictureShadow;
        }
    }

    public bool Buy(int price)
    {
        Wallet wallet = FindObjectOfType<Wallet>();
        _isBuyed = wallet.Buy(price);
        return _isBuyed;
    }

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
    }
}
