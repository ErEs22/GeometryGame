using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int flySpeed = 20;
    [HideInInspector]
    public float lifeTime = 1.5f;
    [HideInInspector]
    public int damage = 0;
    [HideInInspector]
    public int pierceEnemyCount = 0;
    [HideInInspector]
    public int knockBack = 0;
    [HideInInspector]
    public bool isCriticalHit = false;
    [HideInInspector]
    public float lifeStealPercentByWeapon = 0;
    protected Collider2D selfCollider;
    [HideInInspector]
    public GameObject hitVFXPrefab;

    protected virtual void Awake() {
        selfCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(pierceEnemyCount == -1) return;
        Hit(other);
        ShowHitVFX(other);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.explodeSFX);
    }

    protected void DisableProjectileCollider()
    {
        selfCollider.enabled = false;
    }

    protected void EnableProjectileCollider()
    {
        selfCollider.enabled = true;
    }

    protected virtual void Hit(Collider2D otherCollider)
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

    protected virtual void ShowHitVFX(Collider2D otherCollider)
    {
        if(transform.tag != GameTag.PlayerProjectile || !otherCollider.TryGetComponent<ITakeDamage>(out ITakeDamage damageObject)) return;
        Vector3 hitPos = transform.position;
        hitPos.z = -5;
        PoolManager.Release(hitVFXPrefab,hitPos);
    }

    protected void LifeSteal(int damage)
    {
        int stealedHP = EyreUtility.Round(damage * lifeStealPercentByWeapon);
        EventManager.instance.OnUpdatePlayerCurrentHP(stealedHP);
    }

    protected virtual void KnockBackHitObject(GameObject hitObject)
    {
        if(hitObject.GetComponent<Enemy>().enemyType == eEnemyType.Normal)
        {
            if(knockBack == 0) return;
            hitObject.TryGetComponent(out Enemy enemy);
            if (enemy == null) return;
            enemy.canMove = false;
            Vector2 knockBackTagetPos = hitObject.transform.position + (transform.right.normalized * knockBack * 0.1f);
            hitObject.transform.DOMove(knockBackTagetPos, 0.1f).OnComplete(() =>
            {
                enemy.canMove = true;
            });
        }
    }

    public void SetDelayDeativate()
    {
        Invoke(nameof(Deativate), lifeTime);
    }

    public void CancelDelayDeativate()
    {
        CancelInvoke(nameof(Deativate));
    }

    protected virtual void Deativate()
    {
        if (gameObject.activeSelf == false) return;
        //穿透次数为-1代表发射物已经碰撞过，无法再次碰撞造成伤害
        pierceEnemyCount = -1;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Fly();
    }

    protected virtual void Fly()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime, Space.World);
    }
}
