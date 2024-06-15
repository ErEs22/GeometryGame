using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultWeapon;
    [DisplayOnly] public List<Weapon> weapons = new List<Weapon>();
    private PlayerManager playerManager;

    private void Awake() {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    private void OnEnable() {
        EventManager.instance.onGenerateWeaonInInventory += GenerateWeaponsInInventory;
    }

    private void OnDisable() {
        EventManager.instance.onGenerateWeaonInInventory -= GenerateWeaponsInInventory;
    }

    private void Start() {
        
        // Invoke(nameof(ClearWeaponSlot),2);
        // Invoke(nameof(DisableAllWeapon),2);
    }

    private void GenerateWeaponsInInventory(List<Item_Weapon> weapons)
    {
        ClearWeaponSlots();
        weapons.ForEach( weapon =>
        {
            ShopItemData_Weapon_SO itemData = weapon.itemData as ShopItemData_Weapon_SO;
            GenerateWeaponSlot(1,itemData.prefab_Weapon);
        });
    }

    private void ClearWeaponSlots()
    {
        if(weapons.Count <= 0) return;
        for(int i = 0; i < weapons.Count; i++)
        {
            weapons.RemoveAt(i);
            Destroy(weapons[i].gameObject);
            i--;
        }
    }

    private void GenerateWeaponSlot(int slotCount,GameObject prefab_Weapon){
        //清空武器列表
        weapons.Clear();
        Vector3[] pos = EyreUtility.GenerateCirclePoints(transform.position,slotCount);
        for(int i = 0; i < pos.Length; i++){
            GameObject newObject = PoolManager.Release(prefab_Weapon);
            Weapon newWeapon = newObject.GetComponent<Weapon>();
            newWeapon.enemyManager = playerManager.enemyManager;
            weapons.Add(newWeapon);
            newObject.transform.parent = transform;
            newObject.transform.position = pos[i];
        }
    }

    private void DisableAllWeapon(){
        foreach (Weapon weapon in weapons)
        {
            weapon.IsWeaponActive = false;
        }
    }
}
