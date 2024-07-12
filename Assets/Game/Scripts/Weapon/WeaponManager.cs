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
        EventManager.instance.onStartLevel += GenerateWeaponsInInventory;
    }

    private void OnDisable() {
        EventManager.instance.onGenerateWeaonInInventory -= GenerateWeaponsInInventory;
        EventManager.instance.onStartLevel -= GenerateWeaponsInInventory;
    }

    private void Start() {
        
        // Invoke(nameof(ClearWeaponSlot),2);
        // Invoke(nameof(DisableAllWeapon),2);
    }

    private void GenerateWeaponsInInventory()
    {
        GenerateSlotWeapons();
    }

    private void ClearWeaponSlots()
    {
        if(weapons.Count <= 0) return;
        for(int i = 0; i < weapons.Count; i++)
        {
            Weapon weapon = weapons[0];
            weapons.RemoveAt(0);
            Destroy(weapon.gameObject);
        }
    }

    private void GenerateSlotWeapons(){
        //TODO武器列表修改时，不将所有武器清除再重新生成，而是将不存在的武器清除，生成新加入的武器
        List<Inventory_Weapon> oldWeapons = new List<Inventory_Weapon>();
        List<Inventory_Weapon> newWeapons = new List<Inventory_Weapon>();
        Vector3[] pos = EyreUtility.GenerateCirclePoints(transform.position,GameInventory.Instance.inventoryWeapons.Count);
        //移除武器列表里不在背包中的武器
        for(int i = 0; i < weapons.Count; i++){
            Inventory_Weapon currenWeapon = GameInventory.Instance.inventoryWeapons.Find(x => x == weapons[i].inventory_Weapon);
            if(currenWeapon == null)
            {
                Weapon removedWeapon = weapons[i];
                weapons.RemoveAt(i);
                Destroy(removedWeapon.gameObject);
                i--;
            }
        }
        //添加背包中的新武器到武器列表中
        for(int i = 0; i < GameInventory.Instance.inventoryWeapons.Count; i++)
        {
            if(!weapons.Find(x => x.inventory_Weapon == GameInventory.Instance.inventoryWeapons[i]))
            {
                GameObject prefabWeapon = GameInventory.Instance.inventoryWeapons[i].weaponData.prefab_Weapon;
                GameObject newWeaponObject = PoolManager.Release(prefabWeapon);
                Weapon newWeapon = newWeaponObject.GetComponent<Weapon>();
                newWeapon.InitData(GameInventory.Instance.inventoryWeapons[i]);
                newWeapon.enemyManager = playerManager.enemyManager;
                weapons.Add(newWeapon);
                newWeaponObject.transform.parent = transform;
            }
        }
        //为所有武器重新设定位置
        for(int i = 0; i < weapons.Count; i++)
        {
            weapons[i].transform.position = pos[i];
        }
    }

    private void DisableAllWeapon(){
        foreach (Weapon weapon in weapons)
        {
            weapon.IsWeaponActive = false;
        }
    }
}
