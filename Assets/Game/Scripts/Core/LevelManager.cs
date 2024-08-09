using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static int currentLevel = 5;
    private int playerUpgradeCount = 0;
    public static eLevelStatus levelStatus = eLevelStatus.Running;
    public GameObject damageDisplayPrefab;
    EnemyGenerator enemyGenerator;
    EnemyManager enemyManager;
    PlayerState playerState;
    CancellationTokenSource waitForEndTokenSource;
    TweenerCore<float,float,FloatOptions> waitForEndTweenTimer;

    private void Awake()
    {
        enemyGenerator = GetComponent<EnemyGenerator>();
        enemyManager = GetComponent<EnemyManager>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void OnEnable() {
        EventManager.instance.onStartLevel += StartLevel;
        EventManager.instance.onStartGame += StartGame;
        EventManager.instance.onPlayerUpgradeCountIncrease += IncreasePlayerUpgradeCount;
        EventManager.instance.onDamageDisplay += ShowDamageValue;
        EventManager.instance.onGameover += Gameover;
    }

    private void OnDisable() {
        EventManager.instance.onStartLevel -= StartLevel;
        EventManager.instance.onStartGame -= StartGame;
        EventManager.instance.onPlayerUpgradeCountIncrease -= IncreasePlayerUpgradeCount;
        EventManager.instance.onDamageDisplay -= ShowDamageValue;
        EventManager.instance.onGameover -= Gameover;
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
        currentLevel = 5;
        Debug.Log("当前关卡：" + currentLevel);
        EventManager.instance.OnInitPlayerStatus();
        EventManager.instance.OnOpenUI(eUIID.PlayerStatusBar);
        return;
        ClearPlayerUpgradeCount();
        StartLevelCountDown();
        enemyManager.SetCurrentEnemyList();
        StartSpawnEnemy();
        EventManager.instance.OnEnableLocomotionInput();
        EventManager.instance.OnSetPlayerInvincible(false);
    }

    /// <summary>
    /// 开始当前关卡
    /// </summary>
    private void StartLevel()
    {
        currentLevel++;
        Debug.Log("当前关卡：" + currentLevel);
        ClearPlayerUpgradeCount();
        StartLevelCountDown();
        enemyManager.SetCurrentEnemyList();
        StartSpawnEnemy();
        EventManager.instance.OnEnableLocomotionInput();
        EventManager.instance.OnSetPlayerInvincible(true);
    }

    private void StartLevelCountDown()
    {
        UpdateGameAndLevelStatusWhenLevelStart();
        EventManager.instance.OnUICountDown(GetLevelTime(),0,1);
        waitForEndTokenSource = new CancellationTokenSource();
        waitForEndTweenTimer = EyreUtility.SetDelay(1 * GetLevelTime(),()=>
        {
            // try
            // {
            //     await UniTask.Delay(1000 * GetLevelTime(),cancellationToken:waitForEndTokenSource.Token);
            // }
            // catch(Exception ex)
            // {
            //     Debug.Log(ex);
            // }
            Debug.Log("当前关卡倒计时结束，关卡结束！");
            if(GlobalVar.gameStatus == eGameStatus.Ended) return;
            UpdateGameAndLevelStatusWhenLevelEnd();
        });
    }

    private void Gameover()
    {
        waitForEndTweenTimer.Kill();
    }

    private void UpdateGameAndLevelStatusWhenLevelEnd()
    {
        //TODO 游戏结束后场景中还有未清除的物体,控制指定敌人在指定关卡生成
        EventManager.instance.OnDisableLocomotionInput();
        levelStatus = eLevelStatus.Ended;
        if(currentLevel < 20)
        {
            GlobalVar.gameStatus = eGameStatus.SkillUI;
            if(playerUpgradeCount > 0)
            {
                EventManager.instance.OnOpenUI(eUIID.UpgradeMenu);
            }
            else
            {
                EventManager.instance.OnOpenUI(eUIID.ShopMenu);
            }
            EventManager.instance.OnShowUpgradeRewardCount(playerUpgradeCount);
        }
        else if(currentLevel == 20)
        {
            GlobalVar.gameStatus = eGameStatus.Ended;
            EventManager.instance.OnOpenUI(eUIID.FinishMenu);
            EventManager.instance.OnGameover();
            Debug.Log("游戏结束，当前关卡：" + currentLevel + "关");
        }
        EventManager.instance.OnSetPlayerInvincible(true);
        EventManager.instance.OnLevelEnd();
    }
    private void UpdateGameAndLevelStatusWhenLevelStart()
    {
        levelStatus = eLevelStatus.Running;
        GlobalVar.gameStatus = eGameStatus.Running;
    }

    private async void StartSpawnEnemy()
    {
        //TODO 精英和Boss关才能生成对应的精英怪和Boss
        if(levelStatus == eLevelStatus.Ended || GlobalVar.gameStatus == eGameStatus.Ended){
            enemyManager.ClearAllEnemy();
            return;
        }
        int enemySpawnCount = GetLevelSpawnEnemysInWaves();
        if(enemyManager.enemies.Count <= 100)
        {
            switch(currentLevel)
            {
                case 5:
                    enemyGenerator.GenerateEnemysAroundRandomPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.HunterEnemy),enemySpawnCount);
                    await UniTask.Delay(500);
                    for(int i = 0; i < Mathf.FloorToInt(enemySpawnCount / 2); i++)
                    {
                        enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(),1);
                    }
                    await UniTask.Delay(GetLevelSpawnEnemysInterval() - 500);
                break;
                case 10:
                    enemyGenerator.GenerateEnemysAroundRandomPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.FlyEnemy),enemySpawnCount);
                    await UniTask.Delay(500);
                    for(int i = 0; i < Mathf.FloorToInt(enemySpawnCount / 2); i++)
                    {
                        enemyGenerator.GenerateEnemysInRandomPos(enemyManager.GetEnemyInCurrentEnemyListRandomly(),1);
                    }
                    await UniTask.Delay(GetLevelSpawnEnemysInterval() - 500);
                break;
                case 15:
                    enemyGenerator.GenerateEnemysAroundRandomPoint(enemyManager.GetEnemyInCurrentEnemyListByName(EnemyName.HunterEnemy),enemySpawnCount);
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
        }
        else
        {
            await UniTask.Delay(GetLevelSpawnEnemysInterval());
        }
        StartSpawnEnemy();
    }

    private int GetLevelSpawnEnemysInWaves()
    {
        return 8 + currentLevel;
    }

    private int GetLevelSpawnEnemysInterval()
    {
        return EyreUtility.Round(1000 * (2 - (0.02f * currentLevel)));
    }

    private int GetLevelTime()
    {
        int levelTime = 20 + Mathf.Clamp((currentLevel - 1) * 5, 0, 40);
        Debug.Log("当前关卡时间为：" + levelTime + "秒");
        return levelTime;
    }

    private void ShowDamageValue(int damage,GameObject damageObject,bool isCritical){
        DamageDisplay displayComp = PoolManager.Release(damageDisplayPrefab,damageObject.transform.position + new Vector3(1,1,0)).GetComponent<DamageDisplay>();
        displayComp.InitDisplayData(damage,isCritical);
    }
}
