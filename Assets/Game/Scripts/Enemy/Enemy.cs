using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 敌人基类
/// </summary>
public class Enemy : MonoBehaviour, ITakeDamage, IHeal
{
    [SerializeField]
    protected EnemyData_SO enemyData;
    protected float maxHP = 0;
    [DisplayOnly]
    [SerializeField]
    protected float HP = 0;
    private float moveSpeed = 5f;
    protected float MoveSpeed
    {
        get
        {
            return moveSpeed * (1 + speedEffectPercent * 0.01f);
        }
        set
        {
            moveSpeed = value;
        }
    }
    /// <summary>
    /// 速度影响百分比值（整数值 10%=>10）
    /// </summary>
    public int speedEffectPercent = 0;
    protected EnemyManager enemyManager;
    protected float distanceToPlayer;
    protected Vector3 moveDir = Vector3.right;
    /// <summary>
    /// 附近一定距离范围内所有敌人
    /// </summary>
    [SerializeField]
    protected List<Transform> nearbyEnemys = new List<Transform>();
    /// <summary>
    /// 附近一定距离范围内有碰撞危险的所有敌人
    /// </summary>
    [SerializeField]
    protected List<Transform> collisionRiskEnemys = new List<Transform>();

    /// <summary>
    /// NOTE!! Dont add Behaviour in here,this will be call first when poolmanager instantiate this object
    /// If you wan to do something when you instantiate a enemy ,do it in the Init function!!!
    /// </summary>
    private void OnEnable()
    {
    }

    private void Update()
    {
        UpdateEnemyList();
        CaculateDistanceToPlayer();
        UpdateMoveDirection();
        HandleMovement();
    }

    protected virtual void Skill()
    {

    }

    public virtual void Init(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        maxHP = enemyData.HP;
        HP = enemyData.HP;
        MoveSpeed = enemyData.moveSpeed;
    }

    public async void ApplyStatusChangeInTime(StatusType statusType, int percentChange, float effectTime)
    {
        switch (statusType)
        {
            case StatusType.HP:
                break;
            case StatusType.MoveSpeed:
                speedEffectPercent += percentChange;
                break;
            default: break;
        }
        await UniTask.Delay((int)(effectTime * 1000));
        switch(statusType)
        {
            case StatusType.HP:
                break;
            case StatusType.MoveSpeed:
                speedEffectPercent -= percentChange;
                break;
            default:break;
        }
    }

    private void CaculateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(GlobalVar.playerTrans.position, transform.position);
    }

    protected virtual void UpdateMoveDirection()
    {
        // if(Mouse.current.leftButton.isPressed){
        //     moveDir = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.value - transform.position);
        //     moveDir.Normalize();
        // }
        // else
        // {
        //     moveDir = Vector3.right;
        // }
        // return;
        if (distanceToPlayer < 0.1f)
        {
            moveDir = Vector3.zero;
        }
        else
        {
            moveDir = GlobalVar.playerTrans.position - transform.position;
            moveDir.Normalize();
        }
    }

    protected virtual void HandleMovement()
    {
        moveDir.Normalize();
        moveDir *= MoveSpeed;
        //计算集群移动方向
        Vector3 centerVel = GetCenteringVelocity(nearbyEnemys);
        moveDir = Vector3.Lerp(moveDir, moveDir + centerVel * enemyManager.centeringMoveWeight, enemyManager.centeringMoveWeight);
        // moveDir += centerVel * enemyManager.centeringMoveWeight;
        // print("centerVel" + centerVel * enemyManager.centeringMoveWeight);
        Vector3 avoideVel = GetAvoideVelocity(collisionRiskEnemys);
        moveDir = Vector3.Lerp(moveDir, moveDir + avoideVel * enemyManager.avoideMoveWeight, enemyManager.avoideMoveWeight);
        // moveDir += avoideVel * enemyManager.avoideMoveWeight;
        // print("AvoideVel" + avoideVel * enemyManager.avoideMoveWeight);
        Vector3 alignmentVel = GetAlignmentVelocity(nearbyEnemys);
        moveDir = Vector3.Lerp(moveDir, moveDir + alignmentVel * enemyManager.alignmentWeight, enemyManager.alignmentWeight);
        // moveDir += alignmentVel * enemyManager.alignmentWeight;
        // moveDir.Normalize();

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        transform.Translate(transform.right * moveDir.magnitude * Time.deltaTime, Space.World);
        print("Magnitude" + moveDir.magnitude);
    }

    public virtual void TakeDamage(float damage)
    {
        //血量已经小于零则不做计算
        if (HP <= 0) return;
        // Debug.Log(this + " is taking damage,decrease " + damage + "HP");
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

    public void Heal(float healHP)
    {
        HP = Mathf.Clamp(HP + healHP, 0, maxHP);
    }

    #region 集群运动计算

    protected void UpdateEnemyList()
    {
        collisionRiskEnemys.Clear();
        nearbyEnemys.Clear();
        foreach (Enemy enemy in enemyManager.enemies)
        {
            if (enemy == this) continue;
            if (Vector3.Distance(transform.position, enemy.transform.position) < enemyManager.collisionRiskDistanceThreshold)
            {
                collisionRiskEnemys.Add(enemy.transform);
            }
            else if (Vector3.Distance(transform.position, enemy.transform.position) < enemyManager.nearbyDistanceThreshold)
            {
                nearbyEnemys.Add(enemy.transform);
            }
        }
    }

    protected Vector3 GetCenteringVelocity(List<Transform> nearbyEnemys)
    {
        if (nearbyEnemys.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 nearbyEnemysCenterPos = new Vector3();
        foreach (Transform enemy in nearbyEnemys)
        {
            nearbyEnemysCenterPos += enemy.position;
        }
        nearbyEnemysCenterPos /= nearbyEnemys.Count;
        return nearbyEnemysCenterPos - transform.position;
    }

    protected Vector3 GetAvoideVelocity(List<Transform> collisionRiskEnemys)
    {
        if (collisionRiskEnemys.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 avoideVel = new Vector3();
        foreach (Transform enemy in collisionRiskEnemys)
        {
            avoideVel += (transform.position - enemy.position).normalized * MoveSpeed;
        }
        avoideVel /= collisionRiskEnemys.Count;
        return avoideVel;
    }

    protected Vector3 GetAlignmentVelocity(List<Transform> nearbyEnemys)
    {
        if (nearbyEnemys.Count == 0)
        {
            return Vector3.zero;
        }
        Vector3 alignmentVel = new Vector3();
        foreach (Transform enemy in nearbyEnemys)
        {
            alignmentVel += enemy.GetComponent<Enemy>().moveDir;
        }
        alignmentVel /= nearbyEnemys.Count;
        return alignmentVel;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, enemyManager.nearbyDistanceThreshold);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, enemyManager.collisionRiskDistanceThreshold);
    }
}
