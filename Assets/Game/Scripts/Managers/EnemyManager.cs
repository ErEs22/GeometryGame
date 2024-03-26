using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<DefaultEnemy> enemies = new List<DefaultEnemy>();

    public GameObject GetClosetEnemyByPlayer(){
        float distanceBtwEnemyAndPlayer = float.MaxValue;
        float tempDistance = 0;
        GameObject closetEnemy = enemies[0].gameObject;
        foreach (DefaultEnemy enemy in enemies)
        {
            tempDistance = Vector3.Distance(GlobalVar.playerObj.transform.position,enemy.transform.position);
            if(tempDistance < distanceBtwEnemyAndPlayer){
                distanceBtwEnemyAndPlayer = tempDistance;
                closetEnemy = enemy.gameObject;
            }
        }
        return closetEnemy;
    }
}
