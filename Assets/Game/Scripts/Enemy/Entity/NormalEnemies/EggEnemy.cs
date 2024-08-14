using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EggEnemy : Enemy
{
    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        Invoke(nameof(Skill),5);
    }

    public override void Die()
    {
        CancelInvoke(nameof(Skill));
        base.Die();
    }

    public override void DieWithoutAnyDropBonus()
    {
        CancelInvoke(nameof(Skill));
        base.DieWithoutAnyDropBonus();
    }

    protected override void Skill()
    {
        EnemyData_WithSplit_SO data = enemyData as EnemyData_WithSplit_SO;
        enemyGenerator.GenerateEnemy(data.splitEnemyPrefab,transform.position);
        DieWithoutAnyDropBonus();
    }

    protected override void HandleMovement()
    {
        //因为这个敌人是不移动的，所以继承后不写任何逻辑
    }

}
