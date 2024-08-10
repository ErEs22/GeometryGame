using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PredatorBoss : Enemy
{
    float rotateTime = 0;
    float dashDistance = 15f;
    EnemyData_WithTwoProjectile_SO newEnemyData;
    Projectile_Predator[] aroundProjectiles = new Projectile_Predator[9];

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = enemyData as EnemyData_WithTwoProjectile_SO;
        Skill();
    }

    protected override void Update()
    {
        base.Update();
        RotateCirclingProjectiles();
    }

    protected override async void Skill()
    {
        for(int i = 0; i < 9; i++)
        {
            aroundProjectiles[i] = ReleaseRotatingProjectile(0,transform.position);
        }
        while(HP > 0)
        {
            float currentHPPercent = HP / maxHP;
            if(currentHPPercent > 0.5f)
            {
                StageOneAttack();
                await UniTask.Delay(1800);
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
        canMove = false;
        Vector3 towardsPlayerDir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        Vector3 attackTargetPos = transform.position + towardsPlayerDir * dashDistance;
        transform.DOMove(attackTargetPos,0.5f).SetEase(Ease.Linear).OnComplete(()=>
        {
            canMove = true;
        });
    }

    private void StageTwoAttack()
    {
        for(int i = 0; i < 10; i++)
        {
            float randomAngleInRadian = Random.Range(0,359);
            ReleaseSingleProjectile(randomAngleInRadian,transform.position,15);
        }
    }

    private void RotateCirclingProjectiles()
    {
        rotateTime += Time.deltaTime;
        float rotateAngle = 60 * rotateTime;
        rotateAngle *= Mathf.Deg2Rad;
        Vector3 angleDir = new Vector3(Mathf.Sin(rotateAngle),Mathf.Cos(rotateAngle),0);
        for(int i = 0; i < 9; i++)
        {
            float offset = (i * 1.5f) + 3 + ((i / 3) * 4);
            aroundProjectiles[i].transform.position = transform.position + angleDir * offset;
        }
    }

    public override void Die()
    {
        base.Die();
        for(int i = 0; i < 9; i++)
        {
            Destroy(aroundProjectiles[i].gameObject);
        }
    }

    /// <summary>
    /// 释放默认发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected Projectile_Predator ReleaseSingleProjectile(float moveDirRad,Vector3 startPos,int flySpeed = 0)
    {
        Quaternion rotation = Quaternion.AngleAxis(moveDirRad, Vector3.forward);
        Projectile_Predator newProjectile = PoolManager.Release(newEnemyData.projectile1, startPos,rotation).GetComponent<Projectile_Predator>();
        newProjectile.damage = enemyData.damage;
        newProjectile.flySpeed = flySpeed;
        // newProjectile.Attack();
        if(flySpeed != 0)
        {
            newProjectile.lifeTime = 10;
            newProjectile.SetDelayDeativate();
        }
        return newProjectile;
    }

    /// <summary>
    /// 释放环Boss发射物
    /// </summary>
    /// <param name="dir">发射物初始方向</param>
    /// <param name="startPos">发射物初始位置</param>
    protected Projectile_Predator ReleaseRotatingProjectile(float moveDirRad,Vector3 startPos,int flySpeed = 0)
    {
        Quaternion rotation = Quaternion.AngleAxis(moveDirRad, Vector3.forward);
        Projectile_Predator newProjectile = PoolManager.Release(newEnemyData.projectile2, startPos,rotation).GetComponent<Projectile_Predator>();
        newProjectile.damage = enemyData.damage;
        newProjectile.flySpeed = flySpeed;
        // newProjectile.Attack();
        if(flySpeed != 0)
        {
            newProjectile.lifeTime = 10;
            newProjectile.SetDelayDeativate();
        }
        return newProjectile;
    }
}