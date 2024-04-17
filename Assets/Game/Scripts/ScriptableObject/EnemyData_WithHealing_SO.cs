using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData",menuName = "ScriptableObject/Enemy/Enemy_WithHealing")]
public class EnemyData_WithHealing_SO : EnemyData_SO
{
    public int healHP = 10;
    public float healRange = 5;
    public LayerMask healObjectMask;
}