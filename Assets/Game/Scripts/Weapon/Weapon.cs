using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData_SO weaponData;
    public EnemyManager enemyManager;
    [DisplayOnly] public Transform muzzlePoint;
    public bool IsWeaponActive {
        get{
            return isWeaponActive;
        }
        set{
            isWeaponActive = value;
        }
    }
    private bool isWeaponActive = true;
    private float fireCountDown = 0;

    private void OnEnable() {
        muzzlePoint = transform.Find("MuzzlePoint");
    }

    private void Update() {
        //朝向最近的敌人
        TowardsClosetEnemy();
        //检查武器是否激活
        if(isWeaponActive){
            //武器发射
            fireCountDown += Time.deltaTime;
            if(fireCountDown > weaponData.fireInterval){
                fireCountDown = 0;
                ReleaseProjectile();
            }
        }
    }

    private void ReleaseProjectile(){
        GameObject newProjectile = PoolManager.Release(weaponData.projectile,muzzlePoint.position,muzzlePoint.rotation);
        
    }

    private void TowardsClosetEnemy(){
        Vector3 dir = Vector3.Normalize(enemyManager.GetClosetEnemyByPlayer().transform.position - transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
