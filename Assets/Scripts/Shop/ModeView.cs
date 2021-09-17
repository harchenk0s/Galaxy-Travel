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
        if (_shopItem.IsBuyed)
        {
            var gameMode = Instantiate(Object);
            gameMode.name = Object.name;
            _levelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
        }
        else
        {
            if (_shopItem.Buy(_shopItem.Price))
            {
                var gameMode = Instantiate(Object);
                gameMode.name = _shopItem.Name;
                _levelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
                ChangeButtonText("Choose");
            }
        }
    }
}
