using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChargerEnemy : Enemy
{
    private bool isCharging = false;
    private float enableChargeDis = 8f;

    protected override void Skill()
    {
        isCharging = true;
        MoveSpeed = 0;
        var seq = DOTween.Sequence();
        Vector3 targetDir = GlobalVar.playerTrans.position - transform.position;
        targetDir.Normalize();
        float angle = Mathf.Atan2(targetDir.y,targetDir.x) * Mathf.Rad2Deg;
        Vector3 targetPos = transform.position + targetDir * 10;
        targetPos.x = Mathf.Clamp(targetPos.x,-GlobalVar.mapWidth,GlobalVar.mapWidth);
        targetPos.y = Mathf.Clamp(targetPos.y,-GlobalVar.mapHeight,GlobalVar.mapHeight);
        seq.Append(transform.DORotateQuaternion(Quaternion.AngleAxis(angle,Vector3.forward),0.1f));
        seq.Append(transform.DOMove(targetPos, 1).SetDelay(0.5f).OnComplete(() =>
        {
            MoveSpeed = enemyData.moveSpeed;
            isCharging = false;
        }));
        seq.Play();
    }

    protected override void HandleMovement()
    {
        if(isCharging) return;
        //检查与玩家的距离，到了冲刺距离则停止移动，开始冲刺
        if (EyreUtility.DistanceCompare2D(distanceToPlayerSq,enableChargeDis,eCompareSign.Less))
        {
            Skill();
        }
        else
        {
            base.HandleMovement();
        }
    }
}
