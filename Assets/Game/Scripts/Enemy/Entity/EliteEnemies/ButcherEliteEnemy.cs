using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class ButcherEliteEnemy : Enemy
{
    EnemyData_WithProjectile_SO newEnemyData;

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
            if(currentHPPercent > 0.7f)
            {
                StageOneAttack();
                await UniTask.Delay(2000);
            }
            else if(currentHPPercent > 0.4f && currentHPPercent <= 0.7f)
            {
                StageTwoAttack();
                await UniTask.Delay(1500);
            }
            else
            {
                StageThreeAttack();
                await UniTask.Delay(1250);
            }
        }
    }

    private void StageOneAttack()
    {
        Vector3 towardsPlayerDir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        Vector3 firstDir = Quaternion.AngleAxis(45.0f,Vector3.forward) * towardsPlayerDir;
        for(int i = 0; i < 4; i++)
        {
            if(i == 1 || i == 2)
            {
                firstDir = firstDir.normalized * 3.5f;
            }
            else
            {
                firstDir = firstDir.normalized * 3;
            }
            Vector3 targetPos = Quaternion.AngleAxis(i * 90.0f,Vector3.forward) * firstDir + GlobalVar.playerTrans.position;
            ReleaseSingleProjectile(Vector3.Normalize(GlobalVar.playerTrans.position - transform.position),targetPos);
        }
    }

    private void StageTwoAttack()
    {
        for(int i = 0; i < 8; i++)
        {
            Vector3 targetPos = EyreUtility.GetRandomPosAroundCertainPos(GlobalVar.playerTrans.position,5.0f);
            float randomAngleInRadian = Mathf.Deg2Rad * Random.Range(0,359);
            Vector3 randomDir = new Vector3(Mathf.Sin(randomAngleInRadian),Mathf.Cos(randomAngleInRadian),0);
            ReleaseSingleProjectile(randomDir,targetPos);
        }
    }

    private void StageThreeAttack()
    {
        for(int i = 0; i < 3; i++)
        {
            Vector3 targetPos = EyreUtility.GetRandomPosAroundCertainPos(GlobalVar.playerTrans.position,7.0f);
            float randomAngleInRadian = Mathf.Deg2Rad * Random.Range(0,359);
            Vector3 randomDir = new Vector3(Mathf.Sin(randomAngleInRadian),Mathf.Cos(randomAngleInRadian),0);
            ReleaseSingleProjectile(randomDir,targetPos,0.66f);
        }
    }

    /// <summary>
    /// 释放发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected void ReleaseSingleProjectile(Vector3 dir,Vector3 startPos,float damagePercent = 1.0f)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile_Slash newProjectile = PoolManager.Release(newEnemyData.projectile, startPos, rotation).GetComponent<Projectile_Slash>();
        newProjectile.damage = EyreUtility.Round(enemyData.damage * damagePercent);
        newProjectile.Attack();
    }
}