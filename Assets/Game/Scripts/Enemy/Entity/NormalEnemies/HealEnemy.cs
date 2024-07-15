using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : Enemy
{
    GameObject moveTarget;
    EnemyData_WithHealing_SO newEnemyData;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        newEnemyData = (EnemyData_WithHealing_SO)enemyData;
        InvokeRepeating(nameof(Skill), 3, 3);
    }

    protected override void Skill()
    {
        Collider2D[] healAllies = Physics2D.OverlapCircleAll(transform.position, newEnemyData.healRange, newEnemyData.healObjectMask);
        foreach (Collider2D heal in healAllies)
        {
            heal.GetComponent<IHeal>().Heal(newEnemyData.healHP);
        }
    }

    private void TryGetNewMoveTarget()
    {
        if (moveTarget == null || !moveTarget.activeSelf)
        {
            moveTarget = enemyManager.GetEnemyInEnemyListRandomly();
        }
        else if(Vector3.Distance(transform.position,moveTarget.transform.position) < 0.2f)
        {
            moveTarget = enemyManager.GetEnemyInEnemyListRandomly();
        }
    }

    protected override void HandleMovement()
    {
        TryGetNewMoveTarget();
        moveDir = moveTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * MoveSpeed * Time.deltaTime, Space.World);
    }
}
