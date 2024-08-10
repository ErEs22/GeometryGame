using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData",menuName = "ScriptableObject/Enemy/Enemy_WithTwoProjectile")]
public class EnemyData_WithTwoProjectile_SO : EnemyData_SO
{
    public GameObject projectile1;
    public GameObject projectile2;
}