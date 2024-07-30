using UnityEngine;

public class Weapon_Obliterator : Weapon
{
    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos,rotation);
        newProjectile.pierceEnemyCount = 1000;
        return newProjectile;
    }
}