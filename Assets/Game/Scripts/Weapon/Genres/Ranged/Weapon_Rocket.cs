using UnityEngine;

public class Weapon_Rocket : Weapon
{
    public override void InitData(Inventory_Weapon data)
    {
        base.InitData(data);
        fireRange = 600 + (50 * weaponLevel) - 50;
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