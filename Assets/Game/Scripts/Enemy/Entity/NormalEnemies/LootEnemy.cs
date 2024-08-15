using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LootEnemy : Enemy
{
    Vector3 targetPos = Vector3.zero;

    protected override void HandleMovement()
    {
        TrySetNewTargetPos();
        Vector3 dir = targetPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * MoveSpeed * Time.deltaTime, Space.World);
    }

    private void TrySetNewTargetPos()
    {
        if(targetPos == Vector3.zero || Vector3.Distance(targetPos,transform.position) < 0.2f)
        {
            targetPos = EyreUtility.GenerateRandomPosInRectExcludeCircle(transform.position,5.0f);
        }
    }

    public override void Die()
    {
        //TODO 掉落箱子和材料
        //死亡后血量应为零
        HP = 0;
        //释放掉落经验球
        for(int i = 0; i < 8; i++)
        {
            bool isBonusExp = false;
            if(GameCoreData.PlayerProperties.bonusCoin > 0)
            {
                isBonusExp = true;
            }
            PoolManager.Release(dropItem,EyreUtility.GetRandomPosAroundCertainPos(transform.position,1.0f)).GetComponent<ExpBall>().Init(isBonusExp);
        }
        gameObject.SetActive(false);
        enemyManager.enemies.Remove(this);
    }
}
