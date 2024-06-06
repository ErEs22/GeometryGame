using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static int currentLevel = 5;
    private int playerUpgradeCount = 0;
    public static LevelStatus levelStatus = LevelStatus.Running;
    EnemyGenerator enemyGenerator;
    EnemyManager enemyManager;
    PlayerState playerState;

    private void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        enemyManager = GetComponent<EnemyManager>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void OnEnable() {
        EventManager.instance.onStartLevel += StartLevel;
        EventManager.instance.onPlayerUpgradeCountIncrease += IncreasePlayerUpgradeCount;
    }

    private void OnDisable() {
        EventManager.instance.onStartLevel -= StartLevel;
        EventManager.instance.onPlayerUpgradeCountIncrease -= IncreasePlayerUpgradeCount;
    }

    private void Start() {
        StartGame();
    }

    private void IncreasePlayerUpgradeCount()
    {
        playerUpgradeCount++;
    }

    private void ClearPlayerUpgradeCount()
    {
        playerUpgradeCount = 0;
    }

    private void StartGame()
    {
        Debug.Log("当前关卡：" + currentLevel);
        EventManager.instance.OnOpenUI(UIID.PlayerStatusBar);
        EventManager.instance.OnInitPlayerStatus();
        ClearPlayerUpgradeCount();
        StartLevelCountDown();
        enemyManager.SetCurrentEnemyList();
        StartSpawnEnemy();
    }

    /// <summary>
    /// 开始当前关卡
    /// </summary>
    private void StartLevel()
    {
        currentLevel++;
        Debug.Log("当前关卡：" + currentLevel);
        EventManager.instance.OnOpenUI(UIID.PlayerStatusBar);
        ClearPlayerUpgradeCount();
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
            if(playerUpgradeCount > 0)
            {
                EventManager.instance.OnOpenUI(UIID.UpgradeMenu);
                EventManager.instance.OnInitPlayerProperties(playerState.playerData);
            }
            else
            {
                EventManager.instance.OnOpenUI(UIID.ShopMenu);
            }
            EventManager.instance.OnShowUpgradeRewardCount(playerUpgradeCount);
        }
        else if(currentLevel == 20)
        {
            GlobalVar.gameStatus = GameStatus.Ended;
            //TODO 游戏结束，弹出结算页面
            EventManager.instance.OnOpenUI(UIID.FinishMenu);
            Debug.Log("游戏结束，当前关卡：" + currentLevel + "关");
        }
        EventManager.instance.OnLevelEnd();
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

        //TODO三个特殊关卡
        switch(currentLevel)
        {
            case 5:
                enemyGenerator.GenerateEnemysAroundPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.NormalEnemy),enemySpawnCount);
                await UniTask.Delay(500);
                for(int i = 0; i < Mathf.FloorToInt(enemySpawnCount / 2); i++)
                {
                    enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(),1);
                }
                await UniTask.Delay(GetLevelSpawnEnemysInterval() - 500);
            break;
            case 10:
                enemyGenerator.GenerateEnemysAroundPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.FlyEnemy),enemySpawnCount);
                await UniTask.Delay(500);
                for(int i = 0; i < Mathf.FloorToInt(enemySpawnCount / 2); i++)
                {
                    enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(),1);
                }
                await UniTask.Delay(GetLevelSpawnEnemysInterval() - 500);
            break;
            case 15:
                enemyGenerator.GenerateEnemysAroundPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.HunterEnemy),enemySpawnCount);
                await UniTask.Delay(500);
                for(int i = 0; i < Mathf.FloorToInt(enemySpawnCount / 2); i++)
                {
                    enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(),1);
                }
                await UniTask.Delay(GetLevelSpawnEnemysInterval() - 500);
            break;
            default:
                for (int i = 0; i < enemySpawnCount; i++)
                {
                    enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(), 1);
                }
                await UniTask.Delay(GetLevelSpawnEnemysInterval());
            break;
        }
        StartSpawnEnemy();
    }

    private int GetLevelSpawnEnemysInWaves()
    {
        return 8 + currentLevel;
    }

    private int GetLevelSpawnEnemysInterval()
    {
        return (int)(1000 * (2 - (0.02f * currentLevel)));
    }

    private int GetLevelTime()
    {
        int levelTime = 20 + Mathf.Clamp((currentLevel - 1) * 5, 0, 40);
        Debug.Log("当前关卡时间为：" + levelTime + "秒");
        return levelTime;
    }
}
