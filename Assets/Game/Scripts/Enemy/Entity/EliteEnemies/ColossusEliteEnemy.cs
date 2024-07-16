using Cysharp.Threading.Tasks;
using UnityEngine;

public class ColossusEliteEnemy : Enemy
{
    private EnemyData_WithProjectile_SO newEnemyData;

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
            else
            {
                StageTwoAttack();
                await UniTask.Delay(1000);
            }
        }
    }

    private void StageOneAttack()
    {
        for(int i = 0; i < 50; i++)
        {
            Vector3 randomPos = EyreUtility.GetRandomPosAroundCertainPos(GlobalVar.playerTrans.position,25.0f);
            ReleaseSingleProjectile(Vector3.right, randomPos,EyreUtility.GetRandomPosAroundCertainPos(randomPos,3.0f));
        }
    }

    private void StageTwoAttack()
    {
        Vector3 towardsPlayerDir = (GlobalVar.playerTrans.position - transform.position).normalized;
        Vector3[] circlePos = EyreUtility.GetCirclePosAroundPoint(GlobalVar.playerTrans.position,5.0f,10);
        for(int i = 0; i < 10; i++)
        {
            Vector3 projectileTargetPos = circlePos[i] + (towardsPlayerDir * 3);
            ReleaseSingleProjectile(Vector3.right,circlePos[i],projectileTargetPos);
        }
    }

    /// <summary>
    /// 释放发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected void ReleaseSingleProjectile(Vector3 dir,Vector3 startPos,Vector3 targetPos,float damagePercent = 1.0f)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile_Colossus newProjectile = PoolManager.Release(newEnemyData.projectile, startPos, rotation).GetComponent<Projectile_Colossus>();
        newProjectile.damage = EyreUtility.Round(enemyData.damage * damagePercent);
        newProjectile.targetMovePos = targetPos;
        newProjectile.Attack();
    }
}