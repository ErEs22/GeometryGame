using System;
using System.Transactions;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    protected GameObject projectilePrefab;
    [Header("Weapon Data---")]
    [SerializeField][DisplayOnly]
    protected int weaponLevel = 1;
    [SerializeField][DisplayOnly]
    protected int damage;
    [SerializeField][DisplayOnly]
    protected float fireInterval;
    [SerializeField][DisplayOnly]
    protected int fireRange;
    [SerializeField][DisplayOnly]
    protected float criticalMul;
    [SerializeField][DisplayOnly]
    protected float criticalRate;
    [SerializeField][DisplayOnly]
    protected int projectileSpeed;
    [SerializeField][DisplayOnly]
    protected int knockBack;
    [HideInInspector]
    public EnemyManager enemyManager;
    [Header("---")]
    public LayerMask targetLayer;
    protected float currentFireInterval;
    [DisplayOnly] public Transform muzzlePoint;
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
    private bool isCriticalHit = false;

    private void Awake() {
        targetLayer = 1 << 6;
    }

    private void OnEnable()
    {
        muzzlePoint = transform.Find("MuzzlePoint");
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

    public virtual void InitData(ShopItemData_Weapon_SO data,int weaponLevel)
    {
        this.weaponLevel = weaponLevel;
        projectilePrefab = data.prefab_Projectile;
        projectileSpeed = data.projectileSpeed;
        data.itemProperties.ForEach( propertyData =>{
            SetDatabyType(propertyData,data.itemLevel,this.weaponLevel);
        });
    }

    protected virtual void Fire()
    {
        ReleaseSingleProjectile(projectilePrefab, muzzlePoint.position, transform.rotation);
    }

    protected virtual Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = PoolManager.Release(projectile, muzzlePos, rotation).GetComponent<Projectile>();
        newProjectile.flySpeed = projectileSpeed;
        newProjectile.lifeTime = (float)fireRange / (40 * newProjectile.flySpeed);
        newProjectile.damage = CheckIsCriticalHit() ? (int)(damage * criticalMul) : damage ;
        newProjectile.knockBack = knockBack;
        newProjectile.SetDelayDeativate();
        return newProjectile;
    }

    protected bool CheckIsCriticalHit()
    {
        float totalCirticalRate = criticalRate + GameCoreData.PlayerProperties.criticalRate;
        if(EyreUtility.GetChanceResult(totalCirticalRate)){
            return true;
        }
        else
        {
            return false;
        }
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
        Vector3 dir = Vector3.Normalize(GetClosetEnemy().transform.position - transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private bool FindEnemyInRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, fireRange / 40,targetLayer);
        return collider == null ? false : true;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, fireRange / 40);
    }

    protected virtual void SetDatabyType(ShopWeaponPropertyPair propertyPair,int weaponBaseLevel,int weaponCurrentLevel)
    {
        float propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(propertyPair.weaponProperty,propertyPair.propertyValue,weaponBaseLevel,weaponCurrentLevel);
        switch(propertyPair.weaponProperty)
        {
            case eWeaponProperty.Damage:
                damage = (int)propertyValue;
                break;
            case eWeaponProperty.CriticalMul:
                criticalMul = propertyValue;
                break;
            case eWeaponProperty.CriticalRate:
                criticalRate = propertyValue;
                break;
            case eWeaponProperty.FireInterval:
                fireInterval = propertyValue;
                currentFireInterval = fireInterval;
                break;
            case eWeaponProperty.AttackRange:
                fireRange = (int)propertyValue;
                break;
            case eWeaponProperty.KnockBack:
                knockBack = (int)propertyValue;
                break;
            default:break;
        }
    }
}
