using Cysharp.Threading.Tasks;
using UnityEngine;

public class GargoyleEliteEnemy : Enemy
{
    EnemyData_WithProjectile_SO newEnemyData;
    Vector3 targetPos = Vector3.zero;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = enemyData as EnemyData_WithProjectile_SO;
        Skill();
    }

    protected override async void Skill()
    {
        while(HP > 0)
        {
            float currentHPPercent = HP / maxHP;
            if(currentHPPercent > 0.6f)
            {
                StageOneAttack();
                await UniTask.Delay(1500);
            }
            else if(currentHPPercent > 0.3f && currentHPPercent <= 0.6f)
            {
                StageTwoAttack();
                await UniTask.Delay(1600);
            }
            else
            {
                StageThreeAttack();
                await UniTask.Delay(500);
            }
        }
    }

    private void StageOneAttack()
    {
        Vector3[] circlePoints = EyreUtility.GetCirclePosAroundPoint(transform.position,15.0f,25);
        for(int i = 0; i < 25; i++)
        {
            ReleaseSingleProjectile(Vector3.right,circlePoints[i]);
        }
    }

    private void StageTwoAttack()
    {
        for(int i = 0; i < 12; i++)
        {
            Vector3 randomPos = EyreUtility.GetRandomPosAroundCertainPos(transform.position,10.0f);
            ReleaseSingleProjectile(Vector3.right,randomPos);
        }
    }

    private void StageThreeAttack()
    {
        for(int i = 0; i < 3; i++)
        {
            float randomAngleInRadian = Random.Range(0,359) * Mathf.Deg2Rad;
            Vector3 randomDir = new Vector3(Mathf.Sin(randomAngleInRadian),Mathf.Cos(randomAngleInRadian),0);
            ReleaseSingleProjectile(randomDir,transform.position,5);
        }
    }

    /// <summary>
    /// 释放发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected void ReleaseSingleProjectile(Vector3 dir,Vector3 startPos,int flySpeed = 0)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile_Gargoyle newProjectile = PoolManager.Release(newEnemyData.projectile, startPos, rotation).GetComponent<Projectile_Gargoyle>();
        newProjectile.damage = enemyData.damage;
        newProjectile.flySpeed = flySpeed;
        newProjectile.lifeTime = 10;
        newProjectile.Attack();
        if(flySpeed != 0)
        {
            newProjectile.SetDelayDeativate();
        }
    }

    protected override void UpdateMoveDirection()
    {
        float currentHPPercent = HP / maxHP;
        if(currentHPPercent <= 0.6f)
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