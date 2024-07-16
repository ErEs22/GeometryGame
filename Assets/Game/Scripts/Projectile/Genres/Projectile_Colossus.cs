using DG.Tweening;
using UnityEngine;

public class Projectile_Colossus : Projectile
{
    public Vector3 targetMovePos;
    protected override void Awake()
    {
        base.Awake();
        DisableProjectileCollider();
    }

    private void OnEnable() {
        Attack();
    }

    protected override void Fly()
    {
    }

    public void Attack()
    {
        transform.DOMove(targetMovePos,0.8f).OnComplete(()=>
        {
            EnableProjectileCollider();
            transform.DOScale(1.5f,0.2f).SetRelative().OnComplete(()=>
            {
                DisableProjectileCollider();
                transform.DOScale(-1.5f,0.05f).SetRelative().OnComplete(()=>{
                    gameObject.SetActive(false);
                });
            });
        });
    }
}