using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy",fileName = "New EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    public float HP = 10;
    public float moveSpeed = 5;
    public float damage = 1;
}
