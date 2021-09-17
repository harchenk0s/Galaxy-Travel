using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeView : ItemView
{
    public override void Render(ShopItem shopItem)
    {
        base.Render(shopItem);
        BuyButton.onClick.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        if (ShopItem.IsBuyed)
        {
            var gameMode = Instantiate(Object);
            gameMode.name = Object.name;
            LevelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
        }
        else
        {
            if (ShopItem.Buy(ShopItem.Price))
            {
                var gameMode = Instantiate(Object);
                gameMode.name = ShopItem.Name;
                LevelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
                ChangeButtonText("Choose");
            }
        }
    }
}
