using UnityEngine;

public class Weapon_Revolver : Weapon
{
    float changeAmmoTime = 2.0f;
    [SerializeField][DisplayOnly]
    int projectileLeftInAmmo = 6;

    public override void InitData(ShopItemData_Weapon_SO data, int weaponLevel)
    {
        base.InitData(data, weaponLevel);
        switch(weaponLevel)
        {
            case 1:
                changeAmmoTime = 2.0f;
            break;
            case 2:
                changeAmmoTime = 1.8f;
            break;
            case 3:
                changeAmmoTime = 1.65f;
            break;
            case 4:
                changeAmmoTime = 1.55f;
            break;
            default:break;
        }
    }

    protected override void Fire()
    {
        base.Fire();
        Mathf.Clamp(--projectileLeftInAmmo,0,int.MaxValue);
        if(projectileLeftInAmmo <= 0)
        {
            currentFireInterval = changeAmmoTime;
            projectileLeftInAmmo = 6;
        }
        else
        {
            currentFireInterval = fireInterval;
        }
    }
}