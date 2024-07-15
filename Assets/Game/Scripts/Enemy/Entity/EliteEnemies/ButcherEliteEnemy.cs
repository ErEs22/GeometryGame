using Cysharp.Threading.Tasks;
using Unity.Mathematics;
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
                await UniTask.Delay(3000);
            }
            else if(currentHPPercent > 0.4f && currentHPPercent <= 0.7f)
            {
                StageTwoAttack();
                await UniTask.Delay(3000);
            }
            else
            {
                StageThreeAttack();
                await UniTask.Delay(3000);
            }
        }
    }

    private void StageOneAttack()
    {
        Vector3 towardsPlayerDir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        Vector3 firstDir = Quaternion.AngleAxis(45.0f,Vector3.forward) * towardsPlayerDir;
        firstDir *= 2;
        for(int i = 0; i < 4; i++)
        {
            Vector3 targetPos = Quaternion.AngleAxis(i * 90.0f,Vector3.forward) * firstDir + GlobalVar.playerTrans.position;
            ReleaseSingleProjectile(Vector3.Normalize(GlobalVar.playerTrans.position - transform.position),targetPos);
        }
    }

    private void StageTwoAttack()
    {

    }

    private void StageThreeAttack()
    {

    }

    /// <summary>
    /// 释放发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected void ReleaseSingleProjectile(Vector3 dir,Vector3 startPos)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile newProjectile = PoolManager.Release(newEnemyData.projectile, startPos, rotation).GetComponent<Projectile>();
        newProjectile.damage = enemyData.damage;
        newProjectile.SetDelayDeativate();
    }
}