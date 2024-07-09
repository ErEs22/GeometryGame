using DG.Tweening;
using UnityEngine;

public class Weapon_ThiefDagger : Weapon_Melee
{

    protected override void HitObject(Collider2D other)
    {
        if (!isDamagable) return;
        other.TryGetComponent(out ITakeDamage takeDamageComp);
        if (takeDamageComp != null)
        {
            //TODO 是否暴击在每次攻击前决定，确定本次攻击是暴击时，与每个敌人造成的伤害都为暴击伤害
            int finalDamage = CheckIsCriticalHit() ? (int)(damage * criticalMul) : damage;
            takeDamageComp.TakeDamage(finalDamage);
            KnockBackHitObject(other.gameObject);
            bool isDead = takeDamageComp.TakeDamage(damage);
            if(isDead)
            {
                CheckWeaponSkillAvailiable();
            }
        }
    }

    private void CheckWeaponSkillAvailiable()
    {
        //TODO根据武器技能判断是否触发
        switch(weaponLevel)
        {
            case 1:
                if(Random.Range(0f,1f) > 0.5f)
                {
                    GameCoreData.PlayerProperties.GainCoin(1);
                }
            break;
            case 2:
                if(Random.Range(0f,1f) > 0.56f)
                {
                    GameCoreData.PlayerProperties.GainCoin(1);
                }
            break;
            case 3:
                if(Random.Range(0f,1f) > 0.62f)
                {
                    GameCoreData.PlayerProperties.GainCoin(1);
                }
            break;
            case 4:
                if(Random.Range(0f,1f) > 0.8f)
                {
                    GameCoreData.PlayerProperties.GainCoin(1);
                }
            break;
            default:break;
        }
    }
}