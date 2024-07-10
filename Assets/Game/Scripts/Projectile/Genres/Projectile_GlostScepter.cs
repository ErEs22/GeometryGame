using UnityEngine;

public class Projectile_GlostScepter : Projectile
{
    public Weapon_GlostScepter weaponShootBy;

    protected override void Hit(Collider2D otherCollider)
    {
        otherCollider.TryGetComponent(out ITakeDamage damageObject);
        if (damageObject != null)
        {
            bool isDead = damageObject.TakeDamage(damage,isCriticalHit);
            if(isDead)
            {
                weaponShootBy.CheckWeaponSkillAvailiable();
            }
            if(otherCollider.tag == "Enemy")
            {
                //血量吸取
                LifeSteal(damage);
                //击退
                KnockBackHitObject(otherCollider.gameObject);
            }
            if (pierceEnemyCount == 0)
            {
                Deativate();
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
}