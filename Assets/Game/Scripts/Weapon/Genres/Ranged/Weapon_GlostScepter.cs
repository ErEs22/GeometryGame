using UnityEngine;

public class Weapon_GlostScepter : Weapon
{
    [SerializeField]
    int enemyKilled = 0;
    [SerializeField]
    int maxHPAdded = 0;

    public override void InitData(Inventory_Weapon data)
    {
        base.InitData(data);
        fireRange = 400 + (50 * weaponLevel) - 50;
    }

    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_2);
        base.Fire();
    }

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile_GlostScepter newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos, rotation) as Projectile_GlostScepter;
        newProjectile.weaponShootBy = this;
        return newProjectile;
    }

    public void CheckWeaponSkillAvailiable()
    {
        enemyKilled++;
        switch(weaponLevel)
        {
            case 1:
                if(enemyKilled >= 20)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.MaxHP,1);
                    enemyKilled -= 20;
                    maxHPAdded++;
                }
            break;
            case 2:
                if(enemyKilled >= 18)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.MaxHP,1);
                    enemyKilled -= 18;
                    maxHPAdded++;
                }
            break;
            case 3:
                if(enemyKilled >= 16)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.MaxHP,1);
                    enemyKilled -= 16;
                    maxHPAdded++;
                }
            break;
            case 4:
                if(enemyKilled >= 12)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.MaxHP,1);
                    enemyKilled -= 12;
                    maxHPAdded++;
                }
            break;
            default:break;
        }
    }
}