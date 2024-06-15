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

    private void AddWeaponToInventory(Inventory_Weapon weapon)
    {
        inventoryWeapons.Add(weapon);
    }
}