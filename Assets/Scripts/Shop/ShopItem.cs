using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "ShopItems", order = 51)]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _picture;
    [SerializeField] private GameObject _prefab;

    private LevelBuilder _levelBuilder;
    private bool _isBuyed = false;

    public bool IsBuyed => _isBuyed;
    public string Name => _name;
    public int Price => _price;
    public GameObject Prefab => _prefab;

    public void Buy()
    {
        _isBuyed = !_isBuyed;
    }

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
    }
}
