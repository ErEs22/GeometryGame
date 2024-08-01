using UnityEngine;

public class Weapon_Shuriken : Weapon
{

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile_Shuriken newProjectile = PoolManager.Release(projectile, muzzlePos, rotation).GetComponent<Projectile_Shuriken>();
        newProjectile.bounceTimes = weaponLevel;
        newProjectile.isCriticalHit = isCriticalHit;
        newProjectile.flySpeed = projectileSpeed;
        newProjectile.lifeTime = (float)fireRange / (40 * newProjectile.flySpeed);
        newProjectile.damage = isCriticalHit ? EyreUtility.Round(damage * criticalMul) : damage;
        newProjectile.knockBack = knockBack;
        newProjectile.lifeStealPercentByWeapon = lifeSteal;
        newProjectile.SetDelayDeativate();
        return newProjectile;
    }
}