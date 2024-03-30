using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    float thresholdDisToPlayer = 5f;
    Vector2 generateRange = new Vector2(50,50);
    EnemyManager enemyManager;
    Vector2 playerPos;

    private void Start() {
        enemyManager = GetComponent<EnemyManager>();
        playerPos = GlobalVar.playerObj.transform.position;
        GenerateEnemysInRandomPos(15);
        GenerateEnemysAroundPoint(Vector3.zero,15);
    }

    /// <summary>
    /// 获取距离玩家有一定距离的安全位置
    /// </summary>
    /// <returns></returns>
    Vector2 GetPosAwayFromPlayer(){
        float playerLeftSafeDis = playerPos.x - thresholdDisToPlayer + generateRange.x;
        float playerRightSaveDis = generateRange.x - playerPos.x + thresholdDisToPlayer;
        float playerUpSaveDis = generateRange.y - playerPos.y + thresholdDisToPlayer;
        float playerDownSaveDis = playerPos.y - thresholdDisToPlayer + generateRange.y;
        float rangeX,rangeY;
        if(playerLeftSafeDis > playerRightSaveDis){
            rangeX = Random.Range(-generateRange.x,playerPos.x - thresholdDisToPlayer);
        }else{
            rangeX = Random.Range(playerPos.x + thresholdDisToPlayer,generateRange.x);
        }
        if(playerUpSaveDis > playerDownSaveDis){
            rangeY = Random.Range(playerPos.y + thresholdDisToPlayer,generateRange.y);
        }else{
            rangeY = Random.Range(-generateRange.y,playerPos.x - thresholdDisToPlayer);
        }
        return new Vector2(rangeX,rangeY);
    }

    void GenerateEnemysInRandomPos(int count){
        for(int i = 0; i < count; i++){
            Enemy newEnemy = PoolManager.Release(enemy,GetPosAwayFromPlayer()).GetComponent<Enemy>();
            enemyManager.enemies.Add(newEnemy);
            newEnemy.Init(enemyManager);
        }
    }

    void GenerateEnemysAroundPoint(Vector3 center,int count){
        float radius = Mathf.Ceil(count / 6f) + 1;
        for (int i = 0; i < count; i++)
        {
            Enemy newEnemy = PoolManager.Release(enemy,Random.insideUnitCircle * radius).GetComponent<Enemy>();
            enemyManager.enemies.Add(newEnemy);
            newEnemy.Init(enemyManager);
        }
    }
}
