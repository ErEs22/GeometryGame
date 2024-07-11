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
        ClearWeaponSlots();
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
        for(int i = 0; i < pos.Length; i++){
            Weapon currenWeapon = weapons.Find(x => x.inventory_Weapon == GameInventory.Instance.inventoryWeapons[i]);
            Debug.Log(currenWeapon);
            if(currenWeapon != null)
            {
                oldWeapons.Add(GameInventory.Instance.inventoryWeapons[i]);
            }
            else
            {
                newWeapons.Add(GameInventory.Instance.inventoryWeapons[i]);
            }
            GameObject prefabWeapon = GameInventory.Instance.inventoryWeapons[i].weaponData.prefab_Weapon;
            GameObject newWeaponObject = PoolManager.Release(prefabWeapon);
            Weapon newWeapon = newWeaponObject.GetComponent<Weapon>();
            newWeapon.InitData(GameInventory.Instance.inventoryWeapons[i]);
            newWeapon.enemyManager = playerManager.enemyManager;
            weapons.Add(newWeapon);
            newWeaponObject.transform.parent = transform;
            newWeaponObject.transform.position = pos[i];
        }
    }

    private void DisableAllWeapon(){
        foreach (Weapon weapon in weapons)
        {
            weapon.IsWeaponActive = false;
        }
    }
}
