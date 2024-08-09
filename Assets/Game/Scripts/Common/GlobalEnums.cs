using System;
using UnityEngine;

public enum eStatusType
{
    HP,
    MoveSpeed
}

public enum eLevelStatus
{
    Running,
    Ended
}

public enum eGameStatus
{
    Running,
    Pause,
    SkillUI,
    Ended,
    MainMenu
}

public enum eUIID
{
    ShopMenu,
    PauseMenu,
    FinishMenu,
    PlayerStatusBar,
    UpgradeMenu,
    CharacterSelectMenu,
    MainMenu,
}

public enum eCompareSign
{
    Equals,
    Greater,
    Less
}
public enum ePlayerProperty
{
    MaxHP,
    HPRegeneration,
    lifeSteal,
    DamageMul,
    AttackSpeed,
    CriticalRate,
    AttackRange,
    MoveSpeed,
}

public enum eWeaponProperty
{
    Damage,
    CriticalMul,
    FireInterval,
    KnockBack,
    AttackRange,
    LifeSteal,
    DamageThrough,
    Cost,
    CriticalRate,
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
    public static Color ShopItem_Level05 = new Color(1f,0.5f,0,1);
}

[Serializable]
public class ShopPropPropertyPair
{
    public ePlayerProperty playerProperty;
    public int changeAmount;
}

[Serializable]
public class ShopWeaponPropertyPair
{
    public eWeaponProperty weaponProperty;
    public float propertyValue;
}

public enum eShopItemType
{
    Prop,
    Weapon
}

public enum eGameResolution
{
    R_2560X1440 = 0,
    R_1920X1080,
    R_1600X900,
    R_1280X720,
    R_960X540,
    R_800X450,
}

public enum eScreenMode
{
    Windowed = 0,
    BorderlessWindow,
    FullScreen,
}

public enum eFPSOption
{
    FPS_30 = 30,
    FPS_60 = 60,
    FPS_120 = 120,
    FPS_UnLimited
}

public enum eEnemyType
{
    Normal,
    NoMovement,
    Elite,
    Boss,
}
