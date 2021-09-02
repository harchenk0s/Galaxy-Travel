using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipView : MonoBehaviour
{
    [SerializeField] private Text _buttonText;
    [SerializeField] private GameObject _ship;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _name;

    private LevelBuilder _levelBuilder;
    private ShopItem _shopItem;

    public void SetLevelBuilder(LevelBuilder levelBuilder)
    {
        _levelBuilder = levelBuilder;
    }

    public void Render(ShopItem shopItem)
    {
        _shopItem = shopItem;
        _buttonText.text = _shopItem.Price.ToString();
        _ship = _shopItem.Prefab;
        _name.text = _shopItem.Name;
        _buyButton.onClick.AddListener(ChangeShip);
    }

    private void ChangeShip()
    {
        _shopItem.Buy();
        _levelBuilder.ChangeShip(_ship);
    }
}
