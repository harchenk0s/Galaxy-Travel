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
        _shopItem.Buy();
        _object = Instantiate(_object);
        _levelBuilder.ChangeGameMode(_object.GetComponent<GameMode>());
    }
}
