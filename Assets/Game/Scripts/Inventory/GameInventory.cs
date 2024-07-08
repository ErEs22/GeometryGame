using System.Collections.Generic;
using UnityEngine;

public class GameInventory : MonoBehaviour
{
    public static GameInventory Instance;
    public List<Inventory_Weapon> inventoryWeapons = new List<Inventory_Weapon>();
    public List<Inventory_Prop> inventoryProps = new List<Inventory_Prop>();

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
    }

    private void OnDisable() {
    }

    public void AddPropToInventory(Inventory_Prop prop)
    {
        inventoryProps.Add(prop);
    }

    public void AddPropAmount(string propName)
    {
        for(int i = 0; i < inventoryProps.Count; i++)
        {
            if(inventoryProps[i].propData.itemName == propName)
            {
                inventoryProps[i].propAmount++;
                return;
            }
        }
    }

    public void RemovePropFromInventory(string propName)
    {
        for(int i = 0; i < inventoryProps.Count; i++)
        {
            if(inventoryProps[i].propData.itemName == propName)
            {
                inventoryProps.RemoveAt(i);
                return;
            }
        }
    }

    public void AddWeaponToInventory(Inventory_Weapon weapon)
    {
        inventoryWeapons.Add(weapon);
    }

    public void RemoveWeaponFromInventory(string weaponName)
    {
        for(int i = 0; i < inventoryWeapons.Count; i++)
        {
            if(inventoryWeapons[i].weaponData.itemName == weaponName)
            {
                inventoryWeapons.RemoveAt(i);
                return;
            }
        }
    }

    public float CaculateWeaponDataByLevel(eWeaponProperty weaponProperty,float propertyBaseValue,int weaponBaseLevel,int weaponTargetLevel)
    {
        switch(weaponProperty)
        {
            case eWeaponProperty.Damage:
                return propertyBaseValue / weaponBaseLevel * weaponTargetLevel;
            case eWeaponProperty.StealHP:
                return propertyBaseValue;
            case eWeaponProperty.CriticalMul:
                return propertyBaseValue;
            case eWeaponProperty.FireInterval:
                return propertyBaseValue / (1 + (weaponTargetLevel - weaponBaseLevel) * 0.1f);
            case eWeaponProperty.KnockBack:
                return propertyBaseValue;
            case eWeaponProperty.AttackRange:
                return propertyBaseValue;
            case eWeaponProperty.DamageThrough:
                return propertyBaseValue;
            case eWeaponProperty.Cost:
                return propertyBaseValue * Mathf.Pow(2,weaponTargetLevel - weaponBaseLevel);
            default:
                return 0;
        }
    }
}