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

    private void Start() {
        GenerateWeaponSlot(6);
        // Invoke(nameof(ClearWeaponSlot),2);
        // Invoke(nameof(DisableAllWeapon),2);
    }

    private void GenerateWeaponSlot(int slotCount){
        //清空武器列表
        weapons.Clear();
        Vector3[] pos = EyreUtility.GenerateCirclePoints(transform.position,slotCount);
        for(int i = 0; i < pos.Length; i++){
            GameObject newObject = PoolManager.Release(defaultWeapon);
            Weapon newWeapon = newObject.GetComponent<Weapon>();
            newWeapon.enemyManager = playerManager.enemyManager;
            weapons.Add(newWeapon);
            newObject.transform.parent = transform;
            newObject.transform.position = pos[i];
        }
    }

    private void ClearWeaponSlot(){
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void DisableAllWeapon(){
        foreach (Weapon weapon in weapons)
        {
            weapon.IsWeaponActive = false;
        }
    }
}
