using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    EnemyData_WithProjectile_SO newEnemyData;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = (EnemyData_WithProjectile_SO)enemyData;
    }
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
        // Vector3 dir = Vector3.Normalize(GlobalVar.playerObj.position - transform.position);
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Random.Range(0f,359.9f);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile newProjectile = PoolManager.Release(newEnemyData.projectile, transform.position, rotation).GetComponent<Projectile>();
        newProjectile.lifeTime = 10;
        newProjectile.flySpeed = 10;
        newProjectile.damage = enemyData.damage;
        newProjectile.SetDelayDeativate();
    }

    public override bool TakeDamage(int damage,bool isCritical = false)
    {
        Skill();
        return base.TakeDamage(damage);
    }
}
