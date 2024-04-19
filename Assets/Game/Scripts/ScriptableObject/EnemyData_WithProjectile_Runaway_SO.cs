using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData",menuName = "ScriptableObject/Enemy/Enemy_WithProjectile_Runaway")]
public class EnemyData_WithProjectile_Runaway_SO : EnemyData_WithProjectile_SO
{
    public float runawayDistance = 3;
}