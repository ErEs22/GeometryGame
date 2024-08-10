using DG.Tweening;
using UnityEngine;

public class Weapon_Axe : Weapon_Melee
{

    [SerializeField]
    int enemyKilledSinceLastDamageIncrease = 0;
    [SerializeField]
    int damageIncreased = 0;

    protected override void HitObject(Collider2D other)
    {
        if(!isDamagable) return;
        other.TryGetComponent(out ITakeDamage takeDamageComp);
        if(takeDamageComp != null)
        {
            int finalDamage = CheckIsCriticalHit() ? EyreUtility.Round(damage * criticalMul) : damage;
            LifeSteal(finalDamage);
            bool isDead = takeDamageComp.TakeDamage(damage);
            if(isDead)
            {
                CheckWeaponSkillAvailiable();
            }
        }
    }

    private void CheckWeaponSkillAvailiable()
    {
        enemyKilledSinceLastDamageIncrease++;
        switch(weaponLevel)
        {
            case 1:
                if(enemyKilledSinceLastDamageIncrease >= 20)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.DamageMul,1);
                    enemyKilledSinceLastDamageIncrease -= 20;
                    damageIncreased++;
                }
            break;
            case 2:
                if(enemyKilledSinceLastDamageIncrease >= 18)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.DamageMul,1);
                    enemyKilledSinceLastDamageIncrease -= 18;
                    damageIncreased++;
                }
            break;
            case 3:
                if(enemyKilledSinceLastDamageIncrease >= 16)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.DamageMul,1);
                    enemyKilledSinceLastDamageIncrease -= 16;
                    damageIncreased++;
                }
            break;
            case 4:
                if(enemyKilledSinceLastDamageIncrease >= 12)
                {
                    EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.DamageMul,1);
                    enemyKilledSinceLastDamageIncrease -= 12;
                    damageIncreased++;
                }
            break;
            default:break;
        }
    }
}