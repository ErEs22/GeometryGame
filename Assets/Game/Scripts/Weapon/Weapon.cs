using System;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData_SO weaponData;
    public EnemyManager enemyManager;
    public LayerMask targetLayer;
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

    private void OnEnable()
    {
        muzzlePoint = transform.Find("MuzzlePoint");
    }

    private void Update()
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
            if (fireCountDown > weaponData.fireInterval)
            {
                fireCountDown = 0;
                Fire();
            }
        }
    }

    protected virtual void Fire()
    {
        ReleaseSingleProjectile(weaponData.projectile, muzzlePoint.position, transform.rotation);
    }

    protected virtual void ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = PoolManager.Release(projectile, muzzlePos, rotation).GetComponent<Projectile>();
        newProjectile.lifeTime = weaponData.range / weaponData.projectileSpeed;
        newProjectile.flySpeed = weaponData.projectileSpeed;
        newProjectile.damage = weaponData.baseDamage;
        newProjectile.SetDelayDeativate();
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
        Collider2D collider = Physics2D.OverlapCircle(transform.position, weaponData.range,targetLayer);
        return collider == null ? false : true;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, weaponData.range);
    }
}
