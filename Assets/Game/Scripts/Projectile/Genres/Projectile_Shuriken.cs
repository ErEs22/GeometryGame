using UnityEngine;

public class Projectile_Shuriken : Projectile
{
    private float bounceRange = 15f;
    public int bounceTimes = 1;

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
                if(isCriticalHit && bounceTimes > 0)
                {
                    ChangeTarget(otherCollider);
                    bounceTimes--;
                }
                else
                {
                    Deativate();
                }
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }

    /// <summary>
    /// 碰到敌人后更换碰撞目标
    /// </summary>
    /// <param name="currentHitCollider">目前已碰到的目标碰撞体，排除这个碰撞体，防止更换目标为当前碰撞到的目标</param>
    /// <returns>附近是否有合适的目标</returns>
    private bool ChangeTarget(Collider2D currentHitCollider)
    {
        CancelDelayDeativate();
        SetDelayDeativate();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,bounceRange,1 << 6);
        Debug.Log(colliders.Length);
        if(colliders.Length <= 0)
        {
            Debug.Log("No Other Collider Around Here !!!");
            return false;
        }
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy" && collider != currentHitCollider)
            {
                Vector3 targetDir = collider.transform.position - transform.position;
                targetDir.z = 0;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                return true;
            }
        }
        transform.rotation = Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward);
        Debug.Log("No Other Collider Around Here !!!");
        return false;
    }
}