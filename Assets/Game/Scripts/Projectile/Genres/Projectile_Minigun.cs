using UnityEngine;

public class Projectile_Minigun : Projectile
{

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if(damageObject != null)
        {
            damageObject.TakeDamage(damage,isCriticalHit);
            if(otherCollider.tag == "Enemy")
            {
                //血量吸取
                LifeSteal(damage);
                //击退
                KnockBackHitObject(otherCollider.gameObject);
            }
            if(pierceEnemyCount == 0)
            {
                Deativate();
            }
            else
            {
                Mathf.Clamp(--pierceEnemyCount,0,int.MaxValue);
                damage = EyreUtility.Round(damage * 0.5f);
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }
}