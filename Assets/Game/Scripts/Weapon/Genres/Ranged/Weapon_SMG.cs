using UnityEngine;

public class Weapon_SMG : Weapon
{
    private const string path_Transfrom_Barrel = "Model/Weapon_Fixed/Weapon/Head/Barrel";
    private Transform transform_Barrel;
    private float barrelRotateSpeed = 1080f;

    protected override void Awake()
    {
        base.Awake();
        transform_Barrel = transform.Find(path_Transfrom_Barrel);
    }

    protected override void TowardsClosetEnemy()
    {
        base.TowardsClosetEnemy();
        transform_Barrel.Rotate(Vector3.forward,barrelRotateSpeed * Time.deltaTime,Space.Self);
    }
}