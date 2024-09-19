using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterEnemy : Enemy
{
    EnemyData_WithProjectile_SO newEnemyData;
    protected override void Skill()
    {
        InvokeRepeating(nameof(ReleaseSingleProjectile),2,2);
    }

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = (EnemyData_WithProjectile_SO)enemyData;
        Skill();
    }

    protected void ReleaseSingleProjectile()
    {
        Vector3 dir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile newProjectile = PoolManager.Release(newEnemyData.projectile, transform.position, rotation).GetComponent<Projectile>();
        newProjectile.lifeTime = 10;
        newProjectile.flySpeed = 10;
        newProjectile.damage = enemyData.damage;
        newProjectile.SetDelayDeativate();
    }

    public override void Die()
    {
        CancelInvoke(nameof(ReleaseSingleProjectile));
        base.Die();
    }

    public override void DieWithoutAnyDropBonus()
    {
        CancelInvoke(nameof(ReleaseSingleProjectile));
        base.DieWithoutAnyDropBonus();
    }
}
