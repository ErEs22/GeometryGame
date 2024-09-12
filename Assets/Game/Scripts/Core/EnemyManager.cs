using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Range(0.0f,1.0f)]
    public float centeringMoveWeight = 0.3f;
    [Range(0.0f,1.0f)]
    public float avoideMoveWeight = 0.3f;
    [Range(0.0f,1.0f)]
    public float alignmentWeight = 0.3f;
    public float collisionRiskDistanceThreshold = 10;
    public float nearbyDistanceThreshold = 6;
    public List<Enemy> enemies = new List<Enemy>();//游戏内正在活动的敌人
    public List<GameObject> currentLevelEnemys = new List<GameObject>();//当前关卡可生成的敌人种类
    public List<EnemyData_SO> allEliteEnemys = new List<EnemyData_SO>();//游戏所有精英敌人
    public List<EnemyData_SO> allBosses = new List<EnemyData_SO>();//游戏中所有Boss
    public List<EnemyData_SO> allNormalEnemys = new List<EnemyData_SO>();//游戏内所有敌人数据
    // public List<GameObject> allEnemys = new List<GameObject>();//游戏内所有敌人

    private void OnEnable() {
        EventManager.instance.onLevelEnd += ClearAllEnemy;
    }

    private void OnDisable() {
        EventManager.instance.onLevelEnd -= ClearAllEnemy;
    }

    public GameObject GetClosetEnemyByPlayer(){
        float distanceBtwEnemyAndPlayer = float.MaxValue;
        float tempDistance = 0;
        GameObject closetEnemy = enemies[0].gameObject;
        foreach (Enemy enemy in enemies)
        {
            tempDistance = Vector3.Distance(GlobalVar.playerTrans.position,enemy.transform.position);
            if(tempDistance < distanceBtwEnemyAndPlayer){
                distanceBtwEnemyAndPlayer = tempDistance;
                closetEnemy = enemy.gameObject;
            }
        }
        return closetEnemy;
    }

    public GameObject GetEnemyInEnemyListRandomly()
    {
        int randomIndex = Random.Range(0,enemies.Count);
        return enemies[randomIndex].gameObject;
    }

    public void SetCurrentEnemyList()
    {
        currentLevelEnemys.Clear();
        foreach (EnemyData_SO enemy in allNormalEnemys)
        {
            if(enemy.showlevel <= LevelManager.currentLevel)
            {
                // currentLevelEnemys.Add(allEnemys.Find((x => x.name == enemy.name)));
                currentLevelEnemys.Add(enemy.enemyPrefab);
            }
        }
    }

    public GameObject GetEnemyInCurrentEnemyListRandomly()
    {
        int randomIndex = Random.Range(0,currentLevelEnemys.Count);
        return currentLevelEnemys[randomIndex];
    }

    public GameObject GetEliteEnemyInCurrentEnemyList()
    {
        for(int i = 0; i < allEliteEnemys.Count; i++)
        {
            if(allEliteEnemys[i].showlevel == LevelManager.currentLevel)
            {
                return allEliteEnemys[i].enemyPrefab;
            }
        }
        Debug.Log("Current Level Does Not Have Any Enemy To Spawn");
        return null;
    }

    public GameObject GetFirstBoss()
    {
        return allBosses[0].enemyPrefab;
    }

    public GameObject GetSecondBoss()
    {
        return allBosses[1].enemyPrefab;
    }

    public GameObject GetEnemyInCurrentEnemyListByName(string name)
    {
        foreach (var enemy in currentLevelEnemys)
        {
            if(enemy.name == name)
            {
                return enemy;
            }
        }
        Debug.LogWarning("Cant find enemy " + name + " in currentLevelEnemys");
        return null;
    }

    /// <summary>
    /// 清除场景内敌人，无任何掉落
    /// </summary>
    public void ClearAllEnemy()
    {
        for(int i = 0; i < enemies.Count;)
        {
            enemies[i].DieWithoutAnyDropBonus();
        }
    }
}
