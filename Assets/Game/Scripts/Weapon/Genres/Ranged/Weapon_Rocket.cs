using UnityEngine;

public class Weapon_Rocket : Weapon
{
    public override void InitData(ShopItemData_Weapon_SO data, int weaponLevel)
    {
        base.InitData(data, weaponLevel);
        fireRange = 450 + (50 * weaponLevel) - 50;
        switch (weaponLevel)
        {
            case 2:
                fireInterval = 1.78f;
            break;
            case 3:
                fireInterval = 1.37f;
            break;
            case 4:
                fireInterval = 1.03f;
            break;
            default:break;
        }
    }
}