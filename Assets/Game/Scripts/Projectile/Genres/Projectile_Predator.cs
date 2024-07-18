using DG.Tweening;
using UnityEngine;

public class Projectile_Predator : Projectile
{


    protected override void Fly()
    {
        if(flySpeed == 0) return;
        base .Fly();
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
}