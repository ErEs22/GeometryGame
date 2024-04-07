using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData",menuName = "ScriptableObject/Enemy/Enemy_WithProjectile")]
public class EnemyData_WithProjectile_SO : EnemyData_SO
{
    public GameObject projectile;
}
