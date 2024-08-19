using UnityEngine;

public class Weapon_MedicalGun : Weapon
{
    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_1);
        base.Fire();
    }
}