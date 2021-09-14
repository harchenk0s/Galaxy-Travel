using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] protected Text _buttonText;
    [SerializeField] protected GameObject _object;
    [SerializeField] protected Button _buyButton;
    [SerializeField] protected Text _name;
    [SerializeField] protected Image _icon;

    protected LevelBuilder _levelBuilder;
    protected ShopItem _shopItem;

    public void SetLevelBuilder(LevelBuilder levelBuilder)
    {
        _levelBuilder = levelBuilder;
    }

    public virtual void Render(ShopItem shopItem)
    {
        _shopItem = shopItem;
        _shopItem.BuyEvent.AddListener(RefreshIcon);
        _buttonText.text = _shopItem.IsBuyed ? "Choose" : _shopItem.Price.ToString();
        _object = _shopItem.Prefab;
        _name.text = _shopItem.Name;
        _icon.sprite = _shopItem.Picture;
    }

    private void RefreshIcon()
    {
        _icon.sprite = _shopItem.Picture;
    }

    protected void ChangeButtonText(string text)
    {
        _buttonText.text = text;
    }
}
