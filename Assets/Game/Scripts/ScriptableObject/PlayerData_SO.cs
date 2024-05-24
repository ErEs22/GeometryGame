using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player/DefaultData",fileName = "PlayerData", order = 0)]
public class PlayerData_SO : ScriptableObject
{
    
    public int HP = 20;
    public int moveSpeed = 10;
    public int hpRegeneraePerSecond = 0;
    public float stealHPRate = 0.0f;
    public float damageMul = 1.0f;
    public float attakSpeedMul = 1.0f;
    public float criticalRate = 0.0f;
    public int attackRange = 450;
}