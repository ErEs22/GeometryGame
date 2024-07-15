using DG.Tweening;
using UnityEngine;

public class Weapon_Melee : Weapon
{

    /// <summary>
    /// 是否在执行攻击动作中，执行攻击动作时武器不可旋转
    /// </summary>
    protected bool isAttacking = false;
    /// <summary>
    /// 是否可造成伤害，攻击动作的指定时刻可以造成伤害
    /// </summary>
    protected bool isDamagable = false;

    protected override void Update()
    {
        if (isAttacking) return;
        base.Update();
    }

    /// <summary>
    /// 武器开火或攻击，重写需重新添加暴击检测
    /// </summary>
    protected override void Fire()
    {
        //暴击检测
        CheckIsCriticalHit();
        isAttacking = true;
        isDamagable = true;
        Vector3 originPos = transform.localPosition;
        Vector3 targetPos = transform.position + transform.right * 5;
        transform.DOMove(targetPos, 0.1f).OnComplete(() =>
        {
            isDamagable = false;
            transform.DOLocalMove(originPos, 0.1f).OnComplete(() =>
            {
                //攻击动作结束后取消暴击状态
                isCriticalHit = false;
                isAttacking = false;
            });
        });
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        HitObject(other);
    }

    protected virtual void HitObject(Collider2D other)
    {
        if (!isDamagable) return;
        other.TryGetComponent(out ITakeDamage takeDamageComp);
        if (takeDamageComp != null)
        {
            int finalDamage = isCriticalHit ? EyreUtility.Round(damage * criticalMul) : damage;
            LifeSteal(finalDamage);
            takeDamageComp.TakeDamage(finalDamage, isCriticalHit);
            KnockBackHitObject(other.gameObject);
        }
    }

    protected void LifeSteal(int damage)
    {
        int stealedHP = EyreUtility.Round(damage * lifeSteal);
        EventManager.instance.OnUpdatePlayerCurrentHP(stealedHP);
    }

    protected virtual void KnockBackHitObject(GameObject hitObject)
    {
        if(hitObject.GetComponent<Enemy>().enemyType == eEnemyType.Normal)
        {
            if(knockBack == 0) return;
            hitObject.TryGetComponent(out Enemy enemy);
            if (enemy == null) return;
            enemy.canMove = false;
            Vector2 knockBackTagetPos = hitObject.transform.position + (transform.right.normalized * knockBack * 0.1f);
            hitObject.transform.DOMove(knockBackTagetPos, 0.1f).OnComplete(() =>
            {
                enemy.canMove = true;
            });
        }
    }
}