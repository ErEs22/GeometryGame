using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Weapon_Shotgun : Weapon
{
    [Tooltip("单次射击垂直发射方向子弹数量")]
    int projectilePerShot_H = 4;
    [Tooltip("单次射击平行发射方向子弹数量")]
    int projectilePerShot_V = 1;
    [Tooltip("水平于发射方向子弹的发射间隔时间")]
    float verticalProjectilInterval = 0.04f;//单位：秒
    float anglePerProjectile = 5;

    protected override Projectile ReleaseSingleProjectile(GameObject projectile, Vector3 muzzlePos, Quaternion rotation)
    {
        Projectile newProjectile = base.ReleaseSingleProjectile(projectile, muzzlePos,rotation);
        newProjectile.pierceEnemyCount = weaponLevel >= 4 ? 3 : 2;
        return newProjectile;
    }

    protected override void Fire()
    {
        CheckIsCriticalHit();
        float angle = Mathf.Atan2(transform.right.y,transform.right.x) * Mathf.Rad2Deg;
        angle -= anglePerProjectile;
        float timer = 0;
        DOTween.To(() => timer, x => timer = x,1,verticalProjectilInterval).OnStepComplete(() =>{
            for(int j = 0; j < projectilePerShot_H; j++){
                ReleaseSingleProjectile(projectilePrefab,muzzlePoint.position,Quaternion.AngleAxis(angle + j * anglePerProjectile,transform.forward));
            }
        }).SetLoops(projectilePerShot_V);
    }
}
