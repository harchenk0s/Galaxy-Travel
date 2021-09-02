using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : ItemView
{
    public override void Render(ShopItem shopItem)
    {
        base.Render(shopItem);
        _buyButton.onClick.AddListener(ChangeShip);
    }

    private void ChangeShip()
    {
        _shopItem.Buy();
        _levelBuilder.ChangeShip(_object);
    }
}
