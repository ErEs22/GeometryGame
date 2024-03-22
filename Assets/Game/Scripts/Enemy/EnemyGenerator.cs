using System.Collections;
using System.Collections.Generic;
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
        playerPos = enemyManager.player.transform.position;
        GenerateEnemy();
    }

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

    void GenerateEnemy(){
        for(int i = 0; i < 10; i++){
            enemyManager.enemies.Add(PoolManager.Release(enemy,GetPosAwayFromPlayer()).GetComponent<DefaultEnemy>());
        }
    }
}
