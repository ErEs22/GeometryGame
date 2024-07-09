using DG.Tweening;
using UnityEngine;

public class Projectile_Rocket : Projectile
{

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if (damageObject != null)
        {
            damageObject.TakeDamage(damage);
            KnockBackHitObject(otherCollider.gameObject);
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
        //TODO爆炸效果显示
        flySpeed = 0;
        transform.DOScale(10.0f,0.2f).OnComplete(()=>{
            transform.localScale.Set(1,1,1);
            Deativate();
        });
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,4.0f);
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