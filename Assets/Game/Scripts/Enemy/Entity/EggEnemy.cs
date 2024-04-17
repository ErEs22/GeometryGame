using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggEnemy : Enemy
{
    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        Invoke(nameof(Skill),5);
    }

    protected override void Skill()
    {
        //TODO 生成指定敌人
    }

    protected override void HandleMovement()
    {
    }

}
