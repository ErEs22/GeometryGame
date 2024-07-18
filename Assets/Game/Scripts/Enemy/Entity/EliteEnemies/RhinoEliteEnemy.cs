using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RhinoEliteEnemy : Enemy
{

    private EnemyData_WithProjectile_SO newEnemyData;
    private float stageOneAttackDistance = 30.0f;
    private float stageTwoAttackDistance = 20.0f;

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
            if(HP / maxHP >= 0.6f)
            {
                StageOneAttack();
                await UniTask.Delay(3200);
            }
            else
            {
                StageTwoAttack();
                await UniTask.Delay(2500);
            }
        }
    }

    private void StageOneAttack()
    {
        canMove = false;
        Vector3 towardsPlayerDir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        Vector3 attackTargetPos = transform.position + towardsPlayerDir * stageOneAttackDistance;
        transform.DOMove(attackTargetPos,1.2f).SetEase(Ease.Linear).OnComplete(()=>
        {
            canMove = true;
        });
        Vector3 verticalTowardsPlayerDir = Quaternion.AngleAxis(90.0f,Vector3.forward) * towardsPlayerDir;
        ReleaseSingleProjectile(verticalTowardsPlayerDir);
        ReleaseSingleProjectile(-verticalTowardsPlayerDir);
        EyreUtility.SetDelay(0.3f,()=>
        {
            ReleaseSingleProjectile(verticalTowardsPlayerDir);
        }).SetLoops(4);
        EyreUtility.SetDelay(0.3f,()=>
        {
            ReleaseSingleProjectile(-verticalTowardsPlayerDir);
        }).SetLoops(4);
    }

    private void StageTwoAttack()
    {
        Vector3 towardsPlayerDir = Vector3.Normalize(GlobalVar.playerTrans.position - transform.position);
        Vector3 attackTargetPos = transform.position + towardsPlayerDir * stageTwoAttackDistance;
        transform.DOMove(attackTargetPos,1.2f).SetEase(Ease.Linear).OnComplete(()=>
        {
            canMove = true;
        });
        Vector3 verticalTowardsPlayerDir = Quaternion.AngleAxis(90.0f,Vector3.forward) * towardsPlayerDir;
        ReleaseSingleProjectile(verticalTowardsPlayerDir);
        EyreUtility.SetDelay(0.05f,()=>
        {
            ReleaseSingleProjectile(verticalTowardsPlayerDir);
        });
        ReleaseSingleProjectile(-verticalTowardsPlayerDir);
        EyreUtility.SetDelay(0.05f,()=>
        {
            ReleaseSingleProjectile(-verticalTowardsPlayerDir);
        });
        EyreUtility.SetDelay(0.3f,()=>
        {
            ReleaseSingleProjectile(verticalTowardsPlayerDir);
        }).SetLoops(4);
        EyreUtility.SetDelay(0.3f,()=>
        {
            ReleaseSingleProjectile(-verticalTowardsPlayerDir);
        }).SetLoops(4);
    }

    protected void ReleaseSingleProjectile(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Projectile_Spitter newProjectile = PoolManager.Release(newEnemyData.projectile, transform.position, rotation).GetComponent<Projectile_Spitter>();
        newProjectile.lifeTime = 10;
        newProjectile.flySpeed = 20;
        newProjectile.damage = enemyData.damage;
        newProjectile.SetDelayDeativate();
    }
}