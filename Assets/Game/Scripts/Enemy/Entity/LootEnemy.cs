using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEnemy : Enemy
{
    Vector3 targetPos = Vector3.zero;

    protected override void HandleMovement()
    {
        TrySetNewTargetPos();
        Vector3 dir = targetPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * MoveSpeed * Time.deltaTime, Space.World);
    }

    private void TrySetNewTargetPos()
    {
        if(targetPos == Vector3.zero || Vector3.Distance(targetPos,transform.position) < 0.2f)
        {
            targetPos = EyreUtility.GenerateRandomPosInRect();
        }
    }

    protected override void Die()
    {
        //TODO 掉落箱子和材料
        base.Die();
    }
}
