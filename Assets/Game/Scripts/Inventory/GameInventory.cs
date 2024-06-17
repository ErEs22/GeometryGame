using System.Collections.Generic;
using UnityEngine;

public class GameInventory : MonoBehaviour
{
    public static GameInventory Instance;
    public List<Inventory_Weapon> inventoryWeapons = new List<Inventory_Weapon>();

    private void Awake() {
        if(GameInventory.Instance == null)
        {
            GameInventory.Instance = this;
        }
        else
        {
            Destroy(GameInventory.Instance.gameObject);
        }
    }

    private void OnEnable() {
        EventManager.instance.onAddWeaponToGameInventory += AddWeaponToInventory;
    }

    private void OnDisable() {
        EventManager.instance.onAddWeaponToGameInventory -= AddWeaponToInventory;
    }

    public void AddWeaponToInventory(Inventory_Weapon weapon)
    {
        inventoryWeapons.Add(weapon);
    }

    public void RemoveWeaponFromInventory(string weaponName)
    {
        for(int i = 0; i < inventoryWeapons.Count; i++)
        {
            if(inventoryWeapons[i].itemData.itemName == weaponName)
            {
                inventoryWeapons.RemoveAt(i);
            }
        }
    }

    public float CaculateWeaponDataByLevel(WeaponProperty weaponProperty,float propertyBaseValue,int weaponBaseLevel,int weaponTargetLevel)
    {
        switch(weaponProperty)
        {
            case WeaponProperty.Damage:
                return propertyBaseValue / weaponBaseLevel * weaponTargetLevel;
            case WeaponProperty.StealHP:
                return propertyBaseValue;
            case WeaponProperty.CriticalMul:
                return propertyBaseValue;
            case WeaponProperty.FireInterval:
                return propertyBaseValue / (1 + (weaponTargetLevel - weaponBaseLevel) * 0.1f);
            case WeaponProperty.PushBack:
                return propertyBaseValue;
            case WeaponProperty.AttackRange:
                return propertyBaseValue;
            case WeaponProperty.DamageThrough:
                return propertyBaseValue;
            case WeaponProperty.Cost:
                return propertyBaseValue * Mathf.Pow(2,weaponTargetLevel - weaponBaseLevel);
            default:
                return 0;
        }
    }
}