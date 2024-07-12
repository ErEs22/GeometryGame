using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData",menuName = "ScriptableObject/Enemy/Enemy_WithSplit")]
public class EnemyData_WithSplit_SO : EnemyData_SO
{
    public GameObject splitEnemyPrefab;
}