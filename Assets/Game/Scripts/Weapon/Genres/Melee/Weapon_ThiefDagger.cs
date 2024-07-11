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
            KnockBackHitObject(other.gameObject);
            int finalDamage = isCriticalHit ? EyreUtility.Round(damage * criticalMul) : damage;
            LifeSteal(finalDamage);
            bool isDead = takeDamageComp.TakeDamage(finalDamage,isCriticalHit);
            if(isDead)
            {
                CheckWeaponSkillAvailiable();
            }
        }
    }

    private void CheckWeaponSkillAvailiable()
    {
        if(!isCriticalHit) return;
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