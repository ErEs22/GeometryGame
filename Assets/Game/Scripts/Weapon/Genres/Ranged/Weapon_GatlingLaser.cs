using UnityEngine;

public class Weapon_GatlingLaser : Weapon
{
    
    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos,rotation);
        newProjectile.pierceEnemyCount = 3;
        return newProjectile;
    }
}