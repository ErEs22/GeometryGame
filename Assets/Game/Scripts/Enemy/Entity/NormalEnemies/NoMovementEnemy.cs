using DG.Tweening;
using UnityEngine;

public class NoMovementEnemy : Enemy
{
    protected override void HandleMovement()
    {
        
    }

    public override void Die()
    {
        //TODO 掉落箱子和材料/水果
        //死亡后血量应为零
        HP = 0;
        //释放掉落经验球
        for(int i = 0; i < 3; i++)
        {
            PoolManager.Release(dropItem,EyreUtility.GetRandomPosAroundCertainPos(transform.position,1.0f)).GetComponent<ExpBall>().Init();
        }
        gameObject.SetActive(false);
        enemyManager.enemies.Remove(this);
    }
}
