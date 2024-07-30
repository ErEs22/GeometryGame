using DG.Tweening;
using UnityEngine;

public class Weapon_Revolver : Weapon
{
    private const string path_Transform_Barrel = "Model/Weapon_Fixed/Weapon/Head/Barrel";
    private Transform transform_Barrel;
    float changeAmmoTime = 2.0f;
    [SerializeField][DisplayOnly]
    int projectileLeftInAmmo = 6;

    protected override void Awake()
    {
        base.Awake();
        transform_Barrel = transform.Find(path_Transform_Barrel);
    }

    public override void InitData(Inventory_Weapon data)
    {
        base.InitData(data);
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
        transform_Barrel.DOLocalRotate(Vector3.forward * 720f,0.25f,RotateMode.Fast).SetRelative().OnComplete(()=>
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
        });
    }
}