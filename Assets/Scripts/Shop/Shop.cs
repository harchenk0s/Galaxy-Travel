using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> _ships;
    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _itemContainer;

    private LevelBuilder _levelBuilder;

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
    }
    private void Start()
    {
        for (int i = 0; i < _ships.Count; i++)
        {
            AddItem(_ships[i]);
        } 
    }

    private void AddItem(ShopItem ship)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SetLevelBuilder(_levelBuilder);
        view.Render(ship);
    }
}
