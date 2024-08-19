using DG.Tweening;
using UnityEngine;

public class Weapon_Rocket : Weapon
{
    private const string path_Transform_Barrel = "Model/Weapon_Fixed/Weapon/Barrel";
    private Transform transform_Barrel;

    protected override void Awake()
    {
        base.Awake();
        transform_Barrel = transform.Find(path_Transform_Barrel);
    }

    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_4);
        base.Fire();
        Sequence seq = DOTween.Sequence();
        seq.Append(transform_Barrel.DOLocalMoveZ(-0.7f,0.08f).SetRelative());
        seq.Append(transform_Barrel.DOLocalMoveZ(0.7f,0.08f).SetRelative());
        seq.Play();
    }

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