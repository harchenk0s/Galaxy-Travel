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
        if (_shopItem.IsBuyed)
            _levelBuilder.ChangeShip(_object);
        else
        {
            if (_shopItem.Buy(_shopItem.Price))
            {
                _levelBuilder.ChangeShip(_object);
                ChangeButtonText("Choose");
            }  
        }
    }
}
