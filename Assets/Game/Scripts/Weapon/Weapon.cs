using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    private const string path_Transfrom_Weapon = "Model/Weapon_Fixed";
    private const string path_Transfrom_MuzzlePoint = "Model/Weapon_Fixed/MuzzlePoint";
    protected Transform transform_Weapon;
    protected Transform muzzlePoint;
    protected GameObject projectilePrefab;
    [Header("Weapon Data---")]
    [SerializeField]
    [DisplayOnly]
    public Inventory_Weapon inventory_Weapon;
    [SerializeField]
    [DisplayOnly]
    protected int weaponLevel = 1;
    [SerializeField]
    [DisplayOnly]
    protected int damage;
    [SerializeField]
    [DisplayOnly]
    protected float fireInterval;
    [SerializeField]
    [DisplayOnly]
    protected int fireRange;
    [SerializeField]
    [DisplayOnly]
    protected float criticalMul;
    [SerializeField]
    [DisplayOnly]
    protected float criticalRate;
    [SerializeField]
    [DisplayOnly]
    protected int projectileSpeed;
    [SerializeField]
    [DisplayOnly]
    protected int knockBack;
    [SerializeField]
    [DisplayOnly]
    protected float lifeSteal;
    [HideInInspector]
    public EnemyManager enemyManager;
    [Header("---")]
    public LayerMask targetLayer;
    protected float currentFireInterval;
    public bool IsWeaponActive
    {
        get
        {
            return isWeaponActive;
        }
        set
        {
            isWeaponActive = value;
        }
    }
    private bool isWeaponActive = true;
    private float fireCountDown = 0;
    protected bool isCriticalHit = false;

    private void Awake()
    {
        targetLayer = 1 << 6;
        transform_Weapon = transform.Find(path_Transfrom_Weapon);
        muzzlePoint = transform.Find(path_Transfrom_MuzzlePoint);
    }

    private void OnEnable()
    {
        EventManager.instance.onUpdatePlayerProperty += UpdateData;
    }

    private void OnDisable() {
        EventManager.instance.onUpdatePlayerProperty -= UpdateData;
    }

    protected virtual void Update()
    {
        //场景没有敌人或没有敌人在范围内武器不会有任何行为
        if (enemyManager.enemies.Count <= 0 || !FindEnemyInRange()) return;
        //朝向最近的敌人
        TowardsClosetEnemy();
        //检查武器是否激活
        if (isWeaponActive)
        {
            //武器发射
            fireCountDown += Time.deltaTime;
            if (fireCountDown > currentFireInterval)
            {
                fireCountDown = 0;
                Fire();
            }
        }
    }

    public virtual void InitData(Inventory_Weapon data)
    {
        inventory_Weapon = data;
        weaponLevel = data.weaponLevel;
        projectilePrefab = data.weaponData.prefab_Projectile;
        projectileSpeed = data.weaponData.projectileSpeed;
        data.weaponData.itemProperties.ForEach(propertyData =>
        {
            SetDatabyType(propertyData, data.weaponData.itemLevel, weaponLevel);
        });
    }

    private void UpdateData(ePlayerProperty playerProperty,int changeAmount)
    {
        //方法参数无作用，为配合事件绑定才加上的
        inventory_Weapon.weaponData.itemProperties.ForEach(propertyData =>
        {
            SetDatabyType(propertyData,inventory_Weapon.weaponData.itemLevel,weaponLevel);
        });
    }

    protected virtual void Fire()
    {
        CheckIsCriticalHit();
        ReleaseSingleProjectile(projectilePrefab, muzzlePoint.position, transform_Weapon.rotation);
    }

    protected virtual Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = PoolManager.Release(projectile, muzzlePos, rotation).GetComponent<Projectile>();
        newProjectile.isCriticalHit = isCriticalHit;
        newProjectile.flySpeed = projectileSpeed;
        newProjectile.lifeTime = (float)fireRange / (40 * newProjectile.flySpeed);
        newProjectile.damage = isCriticalHit ? EyreUtility.Round(damage * criticalMul) : damage;
        newProjectile.knockBack = knockBack;
        newProjectile.lifeStealPercentByWeapon = lifeSteal;
        newProjectile.SetDelayDeativate();
        return newProjectile;
    }

    protected bool CheckIsCriticalHit()
    {
        float totalCirticalRate = (criticalRate + GameCoreData.PlayerProperties.criticalRate) * 0.01f;
        isCriticalHit = EyreUtility.GetChanceResult(totalCirticalRate);
        return isCriticalHit;
    }

    private GameObject GetClosetEnemy()
    {
        float distanceBtwEnemyAndPlayer = float.MaxValue;
        float tempDistance = 0;
        GameObject closetEnemy = enemyManager.enemies[0].gameObject;
        foreach (Enemy enemy in enemyManager.enemies)
        {
            tempDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (tempDistance < distanceBtwEnemyAndPlayer)
            {
                distanceBtwEnemyAndPlayer = tempDistance;
                closetEnemy = enemy.gameObject;
            }
        }
        return closetEnemy;
    }

    private void TowardsClosetEnemy()
    {
        Vector3 dir = Vector3.Normalize(GetClosetEnemy().transform.position - transform_Weapon.position);
        dir.z = 0;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform_Weapon.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private bool FindEnemyInRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, fireRange / 40, targetLayer);
        return collider == null ? false : true;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, fireRange / 40);
    }

    protected virtual void SetDatabyType(ShopWeaponPropertyPair propertyPair, int weaponBaseLevel, int weaponCurrentLevel)
    {
        float propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(propertyPair.weaponProperty, propertyPair.propertyValue, weaponBaseLevel, weaponCurrentLevel);
        switch (propertyPair.weaponProperty)
        {
            case eWeaponProperty.Damage:
                damage = EyreUtility.Round(propertyValue * (1 + (GameCoreData.PlayerProperties.damageMul * 0.01f)));
                break;
            case eWeaponProperty.CriticalMul:
                criticalMul = propertyValue;
                break;
            case eWeaponProperty.CriticalRate:
                criticalRate = propertyValue + (GameCoreData.PlayerProperties.criticalRate * 0.01f);
                break;
            case eWeaponProperty.FireInterval:
                fireInterval = propertyValue / (1 + GameCoreData.PlayerProperties.attackSpeedMul * 0.01f);
                currentFireInterval = fireInterval;
                break;
            case eWeaponProperty.AttackRange:
                fireRange = EyreUtility.Round(propertyValue + GameCoreData.PlayerProperties.attackRange);
                break;
            case eWeaponProperty.KnockBack:
                knockBack = EyreUtility.Round(propertyValue);
                break;
            case eWeaponProperty.LifeSteal:
                lifeSteal = propertyValue + (GameCoreData.PlayerProperties.lifeSteal * 0.01f);
                break;
            default: break;
        }
    }
}
