using UnityEngine;

public class ShopItemData_SO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public string itemType;
    public int itemCost = 20;
    public int itemLevel = 1;
    public eShopItemType shopItemType;
}