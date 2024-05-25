using UnityEngine;

public class Weapon_Scatter : Weapon
{

    float anglePerProjectile = 5;

    protected override void Fire()
    {
        float angle = Mathf.Atan2(transform.right.y,transform.right.x) * Mathf.Rad2Deg;
        angle -= anglePerProjectile;
        for(int i = 0; i < 3; i++){
            ReleaseSingleProjectile(weaponData.projectile,muzzlePoint.position,Quaternion.AngleAxis(angle + i * anglePerProjectile,transform.forward));
        }
    }
}
