using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile_Slash : Projectile
{

    protected override void Awake()
    {
        base.Awake();
        DisableProjectileCollider();
    }

    private void OnEnable() {
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
        // SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // spriteRenderer.color = Color.white;
        // DisableProjectileCollider();
        // spriteRenderer.DOColor(Color.red, 0.5f).OnComplete(() =>
        // {
        //     spriteRenderer.color = Color.blue;
        //     EnableProjectileCollider();
        //     EyreUtility.SetDelay(0.1f, () =>
        //     {
        //         DisableProjectileCollider();
        //         gameObject.SetActive(false);
        //     });
        // });
        DisableProjectileCollider();
        EyreUtility.SetDelay(0.3f,()=>
        {
            EnableProjectileCollider();
        });
        EyreUtility.SetDelay(0.6f,()=>
        {
            DisableProjectileCollider();
            gameObject.SetActive(false);
        });
    }
}
