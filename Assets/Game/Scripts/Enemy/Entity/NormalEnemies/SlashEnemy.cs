using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SlashEnemy : Enemy
{
    Transform playerTrans;
    Vector3 runAwayPos;
    EnemyData_WithProjectile_Runaway_SO newEnemyData;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = (EnemyData_WithProjectile_Runaway_SO)enemyData;
        playerTrans = GlobalVar.playerTrans;
        Skill();
    }

    protected override async void Skill()
    {
        while(HP > 0)
        {
            Invoke(nameof(ReleaseDamageObject),1);
            await UniTask.Delay(3000);
        }
    }

    private void ReleaseDamageObject()
    {
        GameObject damageObject = PoolManager.Release(newEnemyData.projectile);
        damageObject.GetComponent<Projectile_Slash>().damage = newEnemyData.damage;
        damageObject.GetComponent<Projectile_Slash>().Attack();
        int randomDeg = Random.Range(0,360);
        damageObject.transform.rotation = Quaternion.AngleAxis(randomDeg,Vector3.forward);
        damageObject.transform.position = playerTrans.position;
    }

    protected override void UpdateMoveDirection()
    {
        //TODO优化移动方案，防止在边界条件下抽搐
        if(Vector3.Distance(playerTrans.position,transform.position) < newEnemyData.runawayDistance)
        {
            //Run away
            runAwayPos = playerTrans.position.normalized * -25;
            moveDir = Vector3.Normalize((runAwayPos - transform.position) + (transform.position - playerTrans.position));
        }
        else
        {
            base.UpdateMoveDirection();
        }
    }
}
