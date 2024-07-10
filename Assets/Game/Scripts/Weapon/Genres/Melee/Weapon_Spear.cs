using DG.Tweening;
using UnityEngine;

public class Weapon_Spear : Weapon_Melee
{

    protected override void Fire()
    {
        CheckIsCriticalHit();
        isAttacking = true;
        isDamagable = true;
        Vector3 originPos = transform.localPosition;
        Vector3 targetPos = transform.position + transform.right * 10;
        transform.DOMove(targetPos, 0.1f).OnComplete(() =>
        {
            isDamagable = false;
            transform.DOLocalMove(originPos, 0.1f).OnComplete(() =>
            {
                isCriticalHit = false;
                isAttacking = false;
            });
        });
    }
}