using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : Enemy
{
    GameObject moveTarget;
    Vector3 moveDir;

    public override void Init(EnemyManager enemyManager)
    {
        base.Init(enemyManager);
        InvokeRepeating(nameof(Skill), 3, 3);
    }

    protected override void Skill()
    {
        EnemyData_WithHealing_SO thisEnemyData = (EnemyData_WithHealing_SO)enemyData;
        Collider2D[] healAllies = Physics2D.OverlapCircleAll(transform.position, thisEnemyData.healRange, thisEnemyData.healObjectMask);
        foreach (Collider2D heal in healAllies)
        {
            heal.GetComponent<IHeal>().Heal(thisEnemyData.healHP);
        }
    }

    private void TryGetNewMoveTarget()
    {
        if (moveTarget == null || !moveTarget.activeSelf)
        {
            moveTarget = enemyManager.GetOneRandomEnemyInList();
        }
        else if(Vector3.Distance(transform.position,moveTarget.transform.position) < 0.2f)
        {
            moveTarget = enemyManager.GetOneRandomEnemyInList();
        }
    }

    protected override void HandleMovement()
    {
        TryGetNewMoveTarget();
        moveDir = moveTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
    }
}
