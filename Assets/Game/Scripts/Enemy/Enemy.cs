using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 敌人基类
/// </summary>
public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    protected EnemyData_SO enemyData;
    protected float maxHP = 0;
    protected float HP = 0;
    protected float moveSpeed = 5f;
    private EnemyManager enemyManager;
    protected float distanceToPlayer;

    /// <summary>
    /// NOTE!! Dont add Behaviour in here,this will be call first when poolmanager instantiate this object
    /// If you wan to do something when you instantiate a enemy ,do it in the Init function!!!
    /// </summary>
    private void OnEnable()
    {
    }

    private void Update()
    {
        CaculateDistanceToPlayer();
        MoveToPlayer();
    }

    protected virtual void Skill()
    {

    }

    public virtual void Init(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        maxHP = enemyData.HP;
        HP = enemyData.HP;
        moveSpeed = enemyData.moveSpeed;
    }

    private void CaculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(GlobalVar.playerObj.position,transform.position);
    }

    protected virtual void MoveToPlayer()
    {
        if (distanceToPlayer < 0.1f) return;
        Vector3 dirToPlayer = GlobalVar.playerObj.position - transform.position;
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
    }

    public virtual void TakeDamage(float damage)
    {
        //血量已经小于零则不做计算
        if (HP <= 0) return;
        Debug.Log(this + " is taking damage,decrease " + damage + "HP");
        HP = Mathf.Clamp(HP - damage, 0, maxHP);
        //击中效果
        transform.DOScale(2f, 0.05f).OnComplete(() =>
        {
            transform.DOScale(1f, 0.05f);
        });
        if (HP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        transform.DOScale(2f, 0.05f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            enemyManager.enemies.Remove(this);
        });
    }
}
