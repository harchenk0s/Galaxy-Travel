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
        if (ShopItem.IsBuyed)
        {
            LevelBuilder.ChangeShip(Object.GetComponent<Ship>());
        }
        else
        {
            if (ShopItem.Buy(ShopItem.Price))
            {
                LevelBuilder.ChangeShip(Object.GetComponent<Ship>());
                ChangeButtonText("Choose");
            }  
        }
    }
}
