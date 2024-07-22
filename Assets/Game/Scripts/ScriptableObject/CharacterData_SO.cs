using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Character/DefaultData",fileName = "CharacterData", order = 0)]
public class CharacterData_SO : ScriptableObject
{
    public Sprite characterIcon;
    public GameObject characterPrefab;
    public string characterName;
    public int HP = 20;
    public int hpRegeneratePerSecond = 0;
    public float lifeStealRate = 0.0f;
    public float damageMul = 1.0f;
    public float attackSpeedMul = 1.0f;
    public float criticalRate = 0.0f;
    public int attackRange = 450;
    public float moveSpeed = 10;
}