using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ShopWeapon",fileName = "New ShopItem_Weapon")]
public class ShopItemData_Weapon_SO : ShopItemData_SO
{
    public int projectileSpeed;
    public GameObject prefab_Weapon;
    public GameObject prefab_Projectile;
    public GameObject prefab_HitVFX;
    public List<ShopWeaponPropertyPair> itemProperties = new List<ShopWeaponPropertyPair>();
}