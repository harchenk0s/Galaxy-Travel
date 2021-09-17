using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : ItemView
{
    public override void Render(ShopItem shopItem)
    {
        base.Render(shopItem);
        BuyButton.onClick.AddListener(ChangeShip);
    }

    private void ChangeShip()
    {
        if (_shopItem.IsBuyed)
        {
            _levelBuilder.ChangeShip(Object);
        }
        else
        {
            if (_shopItem.Buy(_shopItem.Price))
            {
                _levelBuilder.ChangeShip(Object);
                ChangeButtonText("Choose");
            }  
        }
    }
}
