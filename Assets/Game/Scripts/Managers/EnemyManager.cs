using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public GameObject GetClosetEnemyByPlayer(){
        float distanceBtwEnemyAndPlayer = float.MaxValue;
        float tempDistance = 0;
        GameObject closetEnemy = enemies[0].gameObject;
        foreach (Enemy enemy in enemies)
        {
            tempDistance = Vector3.Distance(GlobalVar.playerObj.position,enemy.transform.position);
            if(tempDistance < distanceBtwEnemyAndPlayer){
                distanceBtwEnemyAndPlayer = tempDistance;
                closetEnemy = enemy.gameObject;
            }
        }
        return closetEnemy;
    }

    public GameObject GetOneRandomEnemyInList()
    {
        int randomIndex = Random.Range(0,enemies.Count);
        return enemies[randomIndex].gameObject;
    }
}
