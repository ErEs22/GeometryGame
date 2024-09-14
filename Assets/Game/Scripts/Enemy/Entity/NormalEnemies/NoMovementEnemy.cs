using DG.Tweening;
using UnityEngine;

public class NoMovementEnemy : Enemy
{
    protected override void HandleMovement()
    {
        
    }

    public override void Die()
    {
        //死亡后血量应为零
        HP = 0;
        //释放掉落经验球
        for(int i = 0; i < 3; i++)
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
