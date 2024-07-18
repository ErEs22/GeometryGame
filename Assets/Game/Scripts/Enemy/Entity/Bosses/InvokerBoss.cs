using Cysharp.Threading.Tasks;
using UnityEngine;

public class InvokerBoss : Enemy
{
    Vector3 targetPos = Vector3.zero;

    EnemyData_WithProjectile_SO newEnemyData;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = enemyData as EnemyData_WithProjectile_SO;
        Skill();
    }

    protected override async void Skill()
    {
        float currentMoveSpeed = MoveSpeed;
        while(HP > 0)
        {
            float currentHPPercent = HP / maxHP;
            if(currentHPPercent > 0.75f)
            {
                StageOneAttack();
                await UniTask.Delay(3200);
            }
            else if(currentHPPercent > 0.4f && currentHPPercent <= 0.75f)
            {
                StageTwoAttack();
                await UniTask.Delay(3200);
            }
            else
            {
                MoveSpeed = currentMoveSpeed * 1.5f;
                StageThreeAttack();
                await UniTask.Delay(3200);
            }
        }
    }

    private void StageOneAttack()
    {
        for(int i = 0; i < 15; i++)
        {
            Vector3 randomPos = EyreUtility.GetRandomPosAroundCertainPos(GlobalVar.playerTrans.position,10.0f);
            ReleaseSingleProjectile(randomPos);
        }
    }

    private void StageTwoAttack()
    {
        for(int i = 0; i < 25; i++)
        {
            Vector3 randomPos = EyreUtility.GetRandomPosAroundCertainPos(GlobalVar.playerTrans.position,15.0f);
            ReleaseSingleProjectile(randomPos);
        }
    }

    private void StageThreeAttack()
    {
        Vector3[] circlePoints = EyreUtility.GetCirclePosAroundPoint(GlobalVar.playerTrans.position,10.0f,25);
        for(int i = 0; i < 25; i++)
        {
            ReleaseSingleProjectile(circlePoints[i]);
        }
    }

    /// <summary>
    /// 释放发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected void ReleaseSingleProjectile(Vector3 startPos)
    {
        Projectile_Invoker newProjectile = PoolManager.Release(newEnemyData.projectile, startPos).GetComponent<Projectile_Invoker>();
        newProjectile.damage = enemyData.damage;
        newProjectile.Attack();
    }

    protected override void UpdateMoveDirection()
    {
        float currentHPPercent = HP / maxHP;
        if(currentHPPercent > 0.4f)
        {
            if (EyreUtility.DistanceCompare2D(distanceToPlayerSq,0.1f,eCompareSign.Less))
            {
                moveDir = Vector3.zero;
            }
            else
            {
                moveDir = GlobalVar.playerTrans.position - transform.position;
                moveDir.Normalize();
            }
        }
        else
        {
            TrySetNewTargetPos();
            moveDir = targetPos - transform.position;
            moveDir.Normalize();
        }
    }

    private void TrySetNewTargetPos()
    {
        if(targetPos == Vector3.zero || Vector3.Distance(targetPos,transform.position) < 0.2f)
        {
            targetPos = EyreUtility.GenerateRandomPosInRectExcludeCircle(transform.position,5.0f);
        }
    }
}