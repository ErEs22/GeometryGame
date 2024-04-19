using System.Collections;
using System.Collections.Generic;
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
        InvokeRepeating(nameof(Skill),3,3);
    }

    protected override void Skill()
    {
        Invoke(nameof(ReleaseDamageObject),1);
    }

    private void ReleaseDamageObject()
    {
        GameObject damageObject = PoolManager.Release(newEnemyData.projectile);
        damageObject.GetComponent<Projectile_Slash>().damage = newEnemyData.damage;
        damageObject.GetComponent<Projectile_Slash>().SetDelayDeativate();
        int randomDeg = Random.Range(0,360);
        damageObject.transform.rotation = Quaternion.AngleAxis(randomDeg,Vector3.forward);
        damageObject.transform.position = playerTrans.position;
    }

    protected override void HandleMovement()
    {
        if(Vector3.Distance(playerTrans.position,transform.position) < newEnemyData.runawayDistance)
        {
            //Run away
            runAwayPos = playerTrans.position.normalized * -25;
            moveDir = Vector3.Normalize((runAwayPos - transform.position) + (transform.position - playerTrans.position));
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            base.HandleMovement();
        }
    }
}
