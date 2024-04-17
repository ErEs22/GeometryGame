using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : Enemy
{
    Vector3 playerPos;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        playerPos = GlobalVar.playerObj.position;
    }

    protected override void Skill()
    {
    }

    private void ReleaseDamageObject()
    {
        EnemyData_WithProjectile_SO newEnemyData = (EnemyData_WithProjectile_SO)enemyData;
        GameObject damageObject = PoolManager.Release(newEnemyData.projectile);
        int randomDeg = Random.Range(0,360);
        damageObject.transform.rotation = Quaternion.AngleAxis(randomDeg,Vector3.forward);
        //TODO Create the prefab resource and give a close position around player.
    }

    protected override void HandleMovement()
    {
        if(Vector3.Distance(playerPos,transform.position) < 1.0f)
        {
            //Run away
        }
        else
        {
            base.HandleMovement();
        }
    }
}
