using UnityEngine;

public class Weapon_Shuriken : Weapon
{
    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_4);
        base.Fire();
    }

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile_Shuriken newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos,rotation) as Projectile_Shuriken;
        newProjectile.bounceTimes = weaponLevel;
        return newProjectile;
    }
}