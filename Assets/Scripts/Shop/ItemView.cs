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
    protected LevelBuilder LevelBuilder;
    protected ShopItem ShopItem;

    public void SetLevelBuilder(LevelBuilder levelBuilder)
    {
        LevelBuilder = levelBuilder;
    }

    public virtual void Render(ShopItem shopItem)
    {
        ShopItem = shopItem;
        ShopItem.BuyEvent.AddListener(RefreshIcon);
        ButtonText.text = ShopItem.IsBuyed ? "Choose" : ShopItem.Price.ToString();
        Object = ShopItem.Prefab;
        Name.text = ShopItem.Name;
        Icon.sprite = ShopItem.Picture;
    }

    protected void ChangeButtonText(string text)
    {
        ButtonText.text = text;
    }

    private void RefreshIcon()
    {
        Icon.sprite = ShopItem.Picture;
    }
}
