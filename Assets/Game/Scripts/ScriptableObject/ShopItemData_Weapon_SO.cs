using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ShopWeapon",fileName = "New ShopItem_Weapon")]
public class ShopItemData_Weapon_SO : ShopItemData_SO
{
    public List<ShopWeaponPropertyPair> itemProperties = new List<ShopWeaponPropertyPair>();
}