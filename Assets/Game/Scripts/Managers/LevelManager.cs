using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static int currentLevel = 19;
    public static LevelStatus levelStatus = LevelStatus.Running;
    EnemyGenerator enemyGenerator;
    EnemyManager enemyManager;

    private void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void OnEnable() {
        EventManager.instance.onStartLevel += StartLevel;
    }

    private void OnDisable() {
        EventManager.instance.onStartLevel -= StartLevel;
    }

    private void Start() {
        StartLevel();
    }

    /// <summary>
    /// 开始当前关卡
    /// </summary>
    private void StartLevel()
    {
        currentLevel++;
        StartLevelCountDown();
        enemyManager.SetCurrentEnemyList();
        StartSpawnEnemy();
    }

    private async void StartLevelCountDown()
    {
        UpdateGameAndLevelStatusWhenLevelStart();
        EventManager.instance.OnUICountDown(GetLevelTime(),0,1);
        await UniTask.Delay(1000 * GetLevelTime());
        Debug.Log("当前关卡倒计时结束，关卡结束！");
        UpdateGameAndLevelStatusWhenLevelEnd();
    }

    private void UpdateGameAndLevelStatusWhenLevelEnd()
    {
        levelStatus = LevelStatus.Ended;
        enemyManager.ClearAllEnemy();
        if(currentLevel < 20)
        {
            GlobalVar.gameStatus = GameStatus.SkillUI;
            //TODO 当前关卡结束，弹出技能页面，清除屏幕敌人
            EventManager.instance.OnOpenUI(UIID.SkillMenu);
        }
        else if(currentLevel == 20)
        {
            GlobalVar.gameStatus = GameStatus.Ended;
            //TODO 游戏结束，弹出结算页面
            EventManager.instance.OnOpenUI(UIID.FinishMenu);
            Debug.Log("游戏结束，当前关卡：" + currentLevel + "关");
        }
    }
    private void UpdateGameAndLevelStatusWhenLevelStart()
    {
        levelStatus = LevelStatus.Running;
        GlobalVar.gameStatus = GameStatus.Running;
    }

    private async void StartSpawnEnemy()
    {
        if(levelStatus == LevelStatus.Ended) return;
        int enemySpawnCount = GetLevelSpawnEnemysInWaves();
        for (int i = 0; i < enemySpawnCount; i++)
        {
            enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(), 1);
        }
        await UniTask.Delay(GetLevelSpawnEnemysInterval());
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
        Debug.Log("当前关卡时间为：" + levelTime + "秒");
        return levelTime;
    }
}
