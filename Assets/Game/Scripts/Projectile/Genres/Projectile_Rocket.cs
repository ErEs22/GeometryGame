using DG.Tweening;
using UnityEngine;

public class Projectile_Rocket : Projectile
{
    private void OnEnable() {
        EnableProjectileCollider();
    }

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if (damageObject != null)
        {
            damageObject.TakeDamage(damage,isCriticalHit);
            if(otherCollider.tag == "Enemy")
            {
                //血量吸取
                LifeSteal(damage);
                //击退
                KnockBackHitObject(otherCollider.gameObject);
            }
            //碰到敌人后爆炸
            if (pierceEnemyCount == 0)
            {
                Explode();
            }
            else
            {
                Mathf.Clamp(--pierceEnemyCount, 0, int.MaxValue);
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }

    private void Explode()
    {
        DisableProjectileCollider();
        flySpeed = 0;
        // transform.DOScale(10.0f,0.2f).OnComplete(()=>{
        //     transform.localScale.Set(1,1,1);
        //     Deativate();
        // });
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,4.0f,1<<6);
        foreach (Collider2D collider in colliders )
        {
            collider.TryGetComponent(out ITakeDamage takeDamageComp);
            if(takeDamageComp != null)
            {
                takeDamageComp.TakeDamage(damage);
            }
        }
    }
}