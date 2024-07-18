using DG.Tweening;
using UnityEngine;

public class Projectile_Gargoyle : Projectile
{

    private void OnEnable()
    {
        DisableProjectileCollider();
    }

    protected override void Fly()
    {
        if(flySpeed == 0) return;
        base.Fly();
    }

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if (damageObject != null)
        {
            damageObject.TakeDamage(damage, isCriticalHit);
            DisableProjectileCollider();
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }

    public void Attack()
    {
        if(flySpeed != 0) return;
        var seq = DOTween.Sequence();
        seq.Append(EyreUtility.SetDelay(0.7f, () =>
        {
            EnableProjectileCollider();
        }));
        seq.Append(transform.DOScale(2.5f, 0.5f).OnStepComplete(() =>
        {
            DisableProjectileCollider();
        }));
        seq.Append(transform.DOScale(1f, 0.05f).OnStepComplete(() =>
        {
            gameObject.SetActive(false);
        }));
        seq.Play();
    }
}