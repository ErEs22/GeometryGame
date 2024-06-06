using System;
using UnityEngine;

public enum StatusType
{
    HP,
    MoveSpeed
}

public enum LevelStatus
{
    Running,
    Ended
}

public enum GameStatus
{
    Running,
    Pause,
    SkillUI,
    Ended,
    MainMenu
}

public enum UIID
{
    ShopMenu,
    PauseMenu,
    FinishMenu,
    PlayerStatusBar,
    UpgradeMenu,
}

public enum CompareSign
{
    Equals,
    Greater,
    Less
}
public enum PlayerProperty
{
    MaxHP,
    HPRegeneration,
    StealHP,
    DamageMul,
    AttackSpeed,
    CriticalRate,
    AttackRange,
    MoveSpeed,
}

public enum WeaponProperty
{
    Damage,
    CriticalMul,
    FireInterval,
    PushBack,
    AttackRange,
    StealHP,
    DamageThrough
}

public static class EnemyName
{
    public const string NormalEnemy = "NormalEnemy";
    public const string FlyEnemy = "FlyEnemy";
    public const string HunterEnemy = "HunterEnemy";
}

public static class GameTag
{
    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string Enemy = "Enemy";
    public const string EnemyProjectile = "EnemyProjectile";
    public const string PlayerProjectile = "PlayerProjectile";
    public const string DropItem = "DropItem";
}

public static class GameColor
{
    public static Color ShopItem_Locked = Color.white;
    public static Color ShopItem_Level01 = new Color(0, 0, 0, 0);
    public static Color ShopItem_Level02 = Color.blue;
    public static Color ShopItem_Level03 = new Color(160f/255f, 32f/255f, 240f/255f);
    public static Color ShopItem_Level04 = Color.red;
}

[Serializable]
public class ShopPropPropertyPair
{
    public PlayerProperty playerProperty;
    public int changeAmount;
}

[Serializable]
public class ShopWeaponPropertyPair
{
    public WeaponProperty weaponProperty;
    public float propertyValue;
}