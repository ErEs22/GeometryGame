using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int flySpeed = 20;
    [HideInInspector]
    public float lifeTime = 1.5f;
    [HideInInspector]
    public int damage = 0;
    public int pierceEnemyCount = 0;
    public int knockBack = 0;
    public bool isCriticalHit = false;
    public int lifeStealPercentByWeapon = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other);
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

    protected void LifeSteal(int damage)
    {
        float lifeStealPercent = (lifeStealPercentByWeapon + GameCoreData.PlayerProperties.lifeSteal) * 0.01f;
        int stealedHP = (int)(damage * lifeStealPercent);
        EventManager.instance.OnUpdatePlayerProperty(ePlayerProperty.MaxHP, stealedHP);
    }

    protected virtual void KnockBackHitObject(GameObject hitObject)
    {
        hitObject.TryGetComponent(out Enemy enemy);
        if (enemy == null) return;
        enemy.canMove = false;
        Vector2 knockBackTagetPos = hitObject.transform.position + (transform.right.normalized * knockBack * 0.1f);
        hitObject.transform.DOMove(knockBackTagetPos, 0.1f).OnComplete(() =>
        {
            enemy.canMove = true;
        });
    }

    public void SetDelayDeativate()
    {
        Invoke(nameof(Deativate), lifeTime);
    }

    protected void Deativate()
    {
        if (gameObject.activeSelf == false) return;
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
