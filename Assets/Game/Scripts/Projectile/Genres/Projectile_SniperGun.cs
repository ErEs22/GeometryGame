using UnityEngine;

public class Projectile_SniperGun : Projectile
{
    [HideInInspector]
    public GameObject splitProjectile;
    [HideInInspector]
    public bool isSplitedProjectile = false;
    [HideInInspector]
    public int splitProjectilesCount = 5;
    /// <summary>
    /// 狙击子弹分裂前击中的物体，分裂后的子弹应忽略碰撞该物体
    /// </summary>
    private Collider2D hitedCollider = null;

    protected override void Hit(Collider2D otherCollider)
    {
        if(hitedCollider != null && hitedCollider == otherCollider) return;
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if (damageObject != null && otherCollider.tag == "Enemy")
        {
            damageObject.TakeDamage(damage,isCriticalHit);
            //血量吸取
            LifeSteal(damage);
            //击退
            KnockBackHitObject(otherCollider.gameObject);
            if(isSplitedProjectile)
            {
                Deativate();
            }
            else
            {
                Deativate();
                SpawnSplitProjectiles(otherCollider);
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }

    private void SpawnSplitProjectiles(Collider2D hitCollider)
    {
        for(int i = 0; i < splitProjectilesCount; i++)
        {
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward);
            Projectile_SniperGun newProjectile = PoolManager.Release(splitProjectile,transform.position,randomRotation).GetComponent<Projectile_SniperGun>();
            newProjectile.isSplitedProjectile = true;
            newProjectile.hitVFXPrefab = hitVFXPrefab;
            newProjectile.hitedCollider = hitCollider;
            newProjectile.isCriticalHit = isCriticalHit;
            newProjectile.flySpeed = flySpeed;
            newProjectile.lifeTime = lifeTime;
            newProjectile.damage = 5 + splitProjectilesCount - 3;
            newProjectile.knockBack = knockBack;
            newProjectile.lifeStealPercentByWeapon = lifeStealPercentByWeapon;
            newProjectile.pierceEnemyCount = 0;
            newProjectile.SetDelayDeativate();
        }
    }

    protected override void Deativate()
    {
        base.Deativate();
        if(isSplitedProjectile)
        {
            hitedCollider = null;
        }
    }
}