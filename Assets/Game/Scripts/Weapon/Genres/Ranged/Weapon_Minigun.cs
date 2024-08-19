using UnityEngine;

public class Weapon_Minigun : Weapon
{
    private const string path_Transfrom_MuzzlePoint_1 = "Model/Weapon_Fixed/MuzzlePoint_1";
    private const string path_Transfrom_MuzzlePoint_2 = "Model/Weapon_Fixed/MuzzlePoint_2";
    private const string path_Transfrom_Barrel_1 = "Model/Weapon_Fixed/Weapon/Head/Barrel_1";
    private const string path_Transfrom_Barrel_2 = "Model/Weapon_Fixed/Weapon/Head/Barrel_2";
    private Transform transform_MuzzlePoint_1;
    private Transform transform_MuzzlePoint_2;
    private Transform transform_LastMuzzlePoint;
    private Transform transform_Barrel_1;
    private Transform transform_Barrel_2;
    private float barrelRotateSpeed = 1800f;

    protected override void Awake()
    {
        base.Awake();
        transform_MuzzlePoint_1 = transform.Find(path_Transfrom_MuzzlePoint_1);
        transform_MuzzlePoint_2 = transform.Find(path_Transfrom_MuzzlePoint_2);
        transform_Barrel_1 = transform.Find(path_Transfrom_Barrel_1);
        transform_Barrel_2 = transform.Find(path_Transfrom_Barrel_2);
        transform_LastMuzzlePoint = transform_MuzzlePoint_2;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void TowardsClosetEnemy()
    {
        base.TowardsClosetEnemy();
        RotateBarrel();
    }

    private void RotateBarrel()
    {
        transform_Barrel_1.Rotate(Vector3.forward, barrelRotateSpeed * Time.deltaTime, Space.Self);
        transform_Barrel_2.Rotate(Vector3.forward, barrelRotateSpeed * Time.deltaTime, Space.Self);
    }

    public override void InitData(Inventory_Weapon data)
    {
        base.InitData(data);
        switch (weaponLevel)
        {
            case 3:
                fireInterval = 0.09f;
                break;
            case 4:
                fireInterval = 0.07f;
                break;
            default: break;
        }
    }

    protected override void Fire()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.weaponFireSFX_1);
        CheckIsCriticalHit();
        Vector3 muzzlePos = Vector3.zero;
        if (transform_LastMuzzlePoint == transform_MuzzlePoint_2)
        {
            muzzlePos = transform_MuzzlePoint_1.position;
            transform_LastMuzzlePoint = transform_MuzzlePoint_1;
        }
        else if (transform_LastMuzzlePoint == transform_MuzzlePoint_1)
        {
            muzzlePos = transform_MuzzlePoint_2.position;
            transform_LastMuzzlePoint = transform_MuzzlePoint_2;
        }
        ReleaseSingleProjectile(projectilePrefab, muzzlePos, transform_Weapon.rotation);
    }

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos, rotation);
        newProjectile.pierceEnemyCount = weaponLevel >= 4 ? 2 : 1;
        return newProjectile;
    }
}