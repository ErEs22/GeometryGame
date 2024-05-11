using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static int currentLevel = 1;
    public static LevelStatus levelStatus = LevelStatus.Running;
    EnemyGenerator enemyGenerator;
    EnemyManager enemyManager;

    private void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void StartLevel()
    {
        StartLevelCountDown();
        enemyManager.SetCurrentEnemyList();
        StartSpawnEnemy();
    }

    private void StartLevelCountDown()
    {
        UniTask.Delay(1000 * GetLevelTime());
        levelStatus = LevelStatus.Ended;
    }

    private void StartSpawnEnemy()
    {
        if(levelStatus == LevelStatus.Ended) return;
        int enemySpawnCount = GetLevelSpawnEnemysInWaves();
        for (int i = 0; i < enemySpawnCount; i++)
        {
            enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(), 1);
        }
        UniTask.Delay(GetLevelSpawnEnemysInterval());
        StartSpawnEnemy();
    }

    private int GetLevelSpawnEnemysInWaves()
    {
        return 8 + currentLevel;
    }

    private int GetLevelSpawnEnemysInterval()
    {
        return (int)(1000 * (1 - (0.02f * currentLevel)));
    }

    private int GetLevelTime()
    {
        int levelTime = 20 + Mathf.Clamp((currentLevel - 1) * 5, 0, 40);
        return levelTime;
    }
}
