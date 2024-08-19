using UnityEngine;

public class Weapon_SniperGun : Weapon
{
    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_3);
        base.Fire();
    }

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile_SniperGun newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos, rotation) as Projectile_SniperGun;
        newProjectile.isSplitedProjectile = false;
        newProjectile.splitProjectilesCount = weaponLevel > 3 ? 8 : 5;
        newProjectile.splitProjectile = projectile;
        return newProjectile;
    }
}