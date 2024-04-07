using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    protected override void Skill()
    {
        int releaseProjectileCount = 10 + (int)(10 * (HP / maxHP));
        for (int i = 0; i < releaseProjectileCount; i++)
        {
            ReleaseSingleProjectile();
        }
    }

    protected virtual void ReleaseSingleProjectile()
    {
        EnemyData_WithProjectile_SO enemyData_WithProjectile_SO = (EnemyData_WithProjectile_SO)enemyData;
        // Vector3 dir = Vector3.Normalize(GlobalVar.playerObj.position - transform.position);
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Random.Range(0f,359.9f);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile newProjectile = PoolManager.Release(enemyData_WithProjectile_SO.projectile, transform.position, rotation).GetComponent<Projectile>();
        newProjectile.lifeTime = 10;
        newProjectile.flySpeed = 10;
        newProjectile.damage = enemyData.damage;
        newProjectile.SetDelayDeativate();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Skill();
    }
}
