using UnityEngine;

public class Projectile_SniperGun : Projectile
{
    [HideInInspector]
    public GameObject splitProjectile;
    [HideInInspector]
    public bool isSplitedProjectile = false;
    [HideInInspector]
    public int splitProjectilesCount = 5;

    protected override void Hit(Collider2D otherCollider)
    {
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
                SpawnSplitProjectiles();
            }
        }
        else
        {
            Debug.Log("碰到的物体没有继承ITakeDamage接口，若需要处理，请继承该接口");
        }
    }

    private void SpawnSplitProjectiles()
    {
        //TODO 生成分裂子弹时需要防止碰到当前位置敌人
        for(int i = 0; i < splitProjectilesCount; i++)
        {
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0,360),Vector3.forward);
            PoolManager.Release(splitProjectile,transform.position,randomRotation).GetComponent<Projectile_SniperGun>().isSplitedProjectile = true;
        }
    }
}