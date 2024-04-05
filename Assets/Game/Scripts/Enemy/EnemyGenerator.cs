using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    float thresholdDisToPlayer = 5f;
    public GameObject point;
    EnemyManager enemyManager;
    Vector2 playerPos;

    struct RadDis{
        public float radius;
        public int side;
        public RadDis(float radius,int side){
            this.radius = radius;
            this.side = side;
        }
    }

    private void OnEnable()
    {
        SetCrossPoint();
    }

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        playerPos = GlobalVar.playerObj.position;
        GenerateEnemysInRandomPos(15);
        GenerateEnemysAroundPoint(Vector2.zero, 15);
    }

    /// <summary>
    /// 获取距离玩家有一定距离的安全位置
    /// </summary>
    /// <returns></returns>
    Vector2 GetPosAwayFromPlayer()
    {
        float playerLeftSafeDis = playerPos.x - thresholdDisToPlayer + GameConstant.gameActiveRect.x;
        float playerRightSaveDis = GameConstant.gameActiveRect.x - playerPos.x + thresholdDisToPlayer;
        float playerUpSaveDis = GameConstant.gameActiveRect.y - playerPos.y + thresholdDisToPlayer;
        float playerDownSaveDis = playerPos.y - thresholdDisToPlayer + GameConstant.gameActiveRect.y;
        float rangeX, rangeY;
        if (playerLeftSafeDis > playerRightSaveDis)
        {
            rangeX = Random.Range(-GameConstant.gameActiveRect.x, playerPos.x - thresholdDisToPlayer);
        }
        else
        {
            rangeX = Random.Range(playerPos.x + thresholdDisToPlayer, GameConstant.gameActiveRect.x);
        }
        if (playerUpSaveDis > playerDownSaveDis)
        {
            rangeY = Random.Range(playerPos.y + thresholdDisToPlayer, GameConstant.gameActiveRect.y);
        }
        else
        {
            rangeY = Random.Range(-GameConstant.gameActiveRect.y, playerPos.x - thresholdDisToPlayer);
        }
        return new Vector2(rangeX, rangeY);
    }

    /// <summary>
    /// 在指定矩形内生成随机二维位置，排除指定圆内的点
    /// </summary>
    /// <param name="circleCenter">排除圆内点圆的圆心</param>
    /// <param name="circleRadius">排除圆内点圆的半径</param>
    /// <returns></returns>
    Vector2 GenerateRandomPosInRectExcludeCircle(Vector2 circleCenter, float circleRadius)
    {
        //TODO 优化不必要的步骤，补全注释
        float randomAngle = Random.Range(0f, 359.9f) * Mathf.Deg2Rad;
        float minRadius = circleRadius;
        float maxRadius = float.MaxValue;
        List<RadDis> disList = new List<RadDis>();
        //计算圆心到从圆心随机角度射出的直线与x = 50的交点的距离
        float xBoundPositiveDis = 50 / Mathf.Cos(randomAngle);
        disList.Add(new RadDis(xBoundPositiveDis,xBoundPositiveDis > 0 ? 0 : 1));
        float xBoundPositiveYPos = xBoundPositiveDis * Mathf.Sin(randomAngle);
        Vector2 xBoundNegativeCrossPos = new Vector2(50, xBoundPositiveYPos);
        //计算圆心到从圆心随机角度射出的直线与y = 50的交点的距离
        float yBoundPositiveDis = 50 / Mathf.Sin(randomAngle);
        disList.Add(new RadDis(yBoundPositiveDis,yBoundPositiveDis > 0 ? 0 : 1));
        float yBoundPositiveXPos = yBoundPositiveDis * Mathf.Cos(randomAngle);
        Vector2 yBoundPositiveCrossPos = new Vector2(yBoundPositiveXPos, 50);
        //当中较短的距离为矩形内的有效距离
        float effectiveDis1 = xBoundPositiveDis > yBoundPositiveDis ? xBoundPositiveDis : yBoundPositiveDis;

        //计算圆心到从圆心随机角度射出的直线与x = -50的交的距离点
        float xBoundNegativeDis = -50 / Mathf.Cos(randomAngle);
        disList.Add(new RadDis(xBoundNegativeDis,xBoundNegativeDis > 0 ? 0 : 1));
        float xBoundNegativeYPos = xBoundNegativeDis * Mathf.Sin(randomAngle);
        Vector2 xBoundPositiveCrossPos = new Vector2(-50, xBoundNegativeYPos);
        //计算圆心到从圆心随机角度射出的直线与y = -50的交的距离点
        float yBoundNegativeDis = -50 / Mathf.Sin(randomAngle);
        disList.Add(new RadDis(yBoundNegativeDis,yBoundNegativeDis > 0 ? 0 : 1));
        float yBoundNegativeXPos = yBoundNegativeDis * Mathf.Cos(randomAngle);
        Vector2 yBoundNegativeCrossPos = new Vector2(yBoundNegativeXPos, -50);
        //当中较短的距离为矩形内的有效距离
        float effectiveDis2 = xBoundNegativeDis > yBoundNegativeDis ? xBoundNegativeDis : yBoundNegativeDis;
        print(xBoundPositiveDis);
        print(yBoundPositiveDis);
        print(xBoundNegativeDis);
        print(yBoundNegativeDis);

        disList.Sort(delegate(RadDis x,RadDis y){
            return x.radius.CompareTo(y.radius);
        });
        print(randomAngle);
        foreach (var value in disList)
        {
            print(value.radius + "------ " + value.side);
        }
        if (Random.Range(0f, 1f) > 0.5)
        {
            float randomRadius = Random.Range(minRadius,Mathf.Abs(disList[1].radius));
            float angle = randomAngle + Mathf.PI * disList[1].side;
            return new Vector2(randomRadius * Mathf.Cos(angle),randomRadius * Mathf.Sin(angle));
        }
        else
        {
            float randomRadius = Random.Range(minRadius,Mathf.Abs(disList[2].radius));
            float angle = randomAngle + Mathf.PI * disList[2].side;
            return new Vector2(randomRadius * Mathf.Cos(angle),randomRadius * Mathf.Sin(angle));
        }
    }

    void SetCrossPoint()
    {
        for (int i = 0; i < 100; i++)
        {
            Transform point = Instantiate(this.point,transform).transform;
            point.position = GenerateRandomPosInRectExcludeCircle(Vector2.zero,20);
        }
    }

    void GenerateEnemysInRandomPos(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy newEnemy = PoolManager.Release(enemy, GetPosAwayFromPlayer()).GetComponent<Enemy>();
            enemyManager.enemies.Add(newEnemy);
            newEnemy.Init(enemyManager);
        }
    }

    void GenerateEnemysAroundPoint(Vector2 center, int count)
    {
        float radius = Mathf.Ceil(count / 6f) + 1;
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * radius + center;
            Enemy newEnemy = PoolManager.Release(enemy, randomPos).GetComponent<Enemy>();
            enemyManager.enemies.Add(newEnemy);
            newEnemy.Init(enemyManager);
        }
    }
}
