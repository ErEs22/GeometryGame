using UnityEngine;

public class Weapon_Minigun : Weapon
{
    public override void InitData(ShopItemData_Weapon_SO data, int weaponLevel)
    {
        base.InitData(data, weaponLevel);
        switch(weaponLevel)
        {
            case 3:
                fireInterval = 0.09f;
            break;
            case 4:
                fireInterval = 0.07f;
            break;
            default:break;
        }
    }

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos, rotation);
        newProjectile.pierceEnemyCount = weaponLevel >= 4 ? 2 : 1;
        return newProjectile;
    }
}