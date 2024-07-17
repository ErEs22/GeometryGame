using DG.Tweening;
using UnityEngine;

public class Projectile_Colossus : Projectile
{
    public Vector3 targetMovePos;
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        DisableProjectileCollider();
        Attack();
    }

    protected override void Fly()
    {
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
        transform.DOMove(targetMovePos, 1.2f).SetEase(Ease.Linear);
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