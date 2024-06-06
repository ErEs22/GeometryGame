using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ShopProp",fileName = "New ShopItem_Prop")]
public class ShopItemData_Prop_SO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public string itemType = "Prop";
    public List<ShopPropPropertyPair> itemProperties = new List<ShopPropPropertyPair>();
}