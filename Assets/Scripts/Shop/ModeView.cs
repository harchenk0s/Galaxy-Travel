using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeView : ItemView
{
    public override void Render(ShopItem shopItem)
    {
        base.Render(shopItem);
        _buyButton.onClick.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        if (_shopItem.IsBuyed)
        {
            var gameMode = Instantiate(_object);
            gameMode.name = _object.name;
            _levelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
        }
        else
        {
            if (_shopItem.Buy(_shopItem.Price))
            {
                var gameMode = Instantiate(_object);
                gameMode.name = _object.name;
                _levelBuilder.ChangeGameMode(gameMode.GetComponent<GameMode>());
                ChangeButtonText("Choose");
            }
        }
    }
}
