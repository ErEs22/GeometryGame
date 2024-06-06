using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ShopWeapon",fileName = "New ShopItem_Weapon")]
public class ShopItemData_Weapon_SO : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public string itemType;
    public List<ShopWeaponPropertyPair> ItemProperties = new List<ShopWeaponPropertyPair>();
}