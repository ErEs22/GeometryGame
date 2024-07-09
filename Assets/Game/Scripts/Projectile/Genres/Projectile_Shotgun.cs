using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Shotgun : Projectile
{

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent<ITakeDamage>(out ITakeDamage damageObject);
        if(damageObject != null)
        {
            damageObject.TakeDamage(damage);
            KnockBackHitObject(otherCollider.gameObject);
            if(pierceEnemyCount == 0)
            {
                Deativate();
            }
            else
            {
                Mathf.Clamp(--pierceEnemyCount,0,int.MaxValue);
                damage = EyreUtility.Round(damage * 0.7f);
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }
}
