using System.Transactions;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData_SO weaponData;
    public EnemyManager enemyManager;
    [DisplayOnly] public Transform muzzlePoint;
    public bool IsWeaponActive {
        get{
            return isWeaponActive;
        }
        set{
            isWeaponActive = value;
        }
    }
    private bool isWeaponActive = true;
    private float fireCountDown = 0;

    private void OnEnable() {
        muzzlePoint = transform.Find("MuzzlePoint");
    }

    private void Update() {
        //朝向最近的敌人
        TowardsClosetEnemy();
        //检查武器是否激活
        if(isWeaponActive){
            //武器发射
            fireCountDown += Time.deltaTime;
            if(fireCountDown > weaponData.fireInterval){
                fireCountDown = 0;
                Fire();
            }
        }
    }

    protected virtual void Fire(){
        ReleaseSingleProjectile(weaponData.projectile,muzzlePoint.position,transform.rotation);
    }

    protected virtual void ReleaseSingleProjectile(GameObject projectile,Vector3 muzzlePos,Quaternion rotation){
        Projectile newProjectile = PoolManager.Release(projectile,muzzlePos,rotation).GetComponent<Projectile>();
        newProjectile.lifeTime = weaponData.range / weaponData.projectileSpeed;
        newProjectile.flySpeed = weaponData.projectileSpeed;
        newProjectile.SetDelayDeativate();
    }

    private GameObject GetClosetEnemy(){
        float distanceBtwEnemyAndPlayer = float.MaxValue;
        float tempDistance = 0;
        GameObject closetEnemy = enemyManager.enemies[0].gameObject;
        foreach (DefaultEnemy enemy in enemyManager.enemies)
        {
            tempDistance = Vector3.Distance(transform.position,enemy.transform.position);
            if(tempDistance < distanceBtwEnemyAndPlayer){
                distanceBtwEnemyAndPlayer = tempDistance;
                closetEnemy = enemy.gameObject;
            }
        }
        return closetEnemy;
    }

    private void TowardsClosetEnemy(){
        if(enemyManager.enemies.Count <= 0) return;
        Vector3 dir = Vector3.Normalize(GetClosetEnemy().transform.position - transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
