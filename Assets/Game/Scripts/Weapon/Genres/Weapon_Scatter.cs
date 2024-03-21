using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon_Scatter : Weapon
{

    float anglePerProjectile = 5;

    protected override void ReleaseProjectile()
    {
        float angle = Mathf.Atan2(transform.right.y,transform.right.x) * Mathf.Rad2Deg;
        angle -= anglePerProjectile;
        for(int i = 0; i < 3; i++){
            GameObject newProjectile = PoolManager.Release(weaponData.projectile,muzzlePoint.position,Quaternion.AngleAxis(angle + i * anglePerProjectile,transform.forward));
            newProjectile.GetComponent<Projectile>().lifeTime = weaponData.range / weaponData.projectileSpeed;
            print("AAAAAAAAAAAAAAA");
        }
    }
}
