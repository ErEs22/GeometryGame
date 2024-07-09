using DG.Tweening;
using UnityEngine;

public class Weapon_Melee : Weapon
{

    protected bool isAttacking = false;
    protected bool isDamagable = false;

    protected override void Update()
    {
        if (isAttacking) return;
        base.Update();
    }

    protected override void Fire()
    {
        isAttacking = true;
        isDamagable = true;
        Vector3 originPos = transform.localPosition;
        Vector3 targetPos = transform.position + transform.right * 5;
        transform.DOMove(targetPos, 0.1f).OnComplete(() =>
        {
            isDamagable = false;
            transform.DOLocalMove(originPos, 0.1f).OnComplete(() =>
            {
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
            int finalDamage = CheckIsCriticalHit() ? (int)(damage * criticalMul) : damage;
            takeDamageComp.TakeDamage(finalDamage);
            KnockBackHitObject(other.gameObject);
        }
    }
    
    protected virtual void KnockBackHitObject(GameObject hitObject)
    {
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