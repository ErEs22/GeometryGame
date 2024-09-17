using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy/Enemy_Default",fileName = "New EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    public new string name;
    public float HP = 10;
    public float moveSpeed = 5;
    public int damage = 1;
    public int showlevel = 1;
    public bool isTowardsPlayer = true;
    public int hpIncreasePerWave = 0;
    public int damageIncreasePerWave = 0;
    public eEnemyType enemyType = eEnemyType.Normal;
    public GameObject enemyPrefab;
}
