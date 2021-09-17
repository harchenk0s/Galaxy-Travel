using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] protected Text ButtonText;
    [SerializeField] protected Button BuyButton;
    [SerializeField] protected Text Name;
    [SerializeField] protected Image Icon;

    protected GameObject Object;
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
        ButtonText.text = _shopItem.IsBuyed ? "Choose" : _shopItem.Price.ToString();
        Object = _shopItem.Prefab;
        Name.text = _shopItem.Name;
        Icon.sprite = _shopItem.Picture;
    }

    protected void ChangeButtonText(string text)
    {
        ButtonText.text = text;
    }

    private void RefreshIcon()
    {
        Icon.sprite = _shopItem.Picture;
    }
}
