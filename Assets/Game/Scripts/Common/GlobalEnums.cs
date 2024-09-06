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
    public static Color ShopItem_Level01 = new Color(1f,1f,1f,1f);
    public static Color ShopItem_Level02 = new Color(0f,1f,239f/255f,1f);
    public static Color ShopItem_Level03 = new Color(183f/256f, 0, 1f,1f);
    public static Color ShopItem_Level04 = new Color(1f,0,13f/256f,1f);
    public static Color ShopItem_Level05 = new Color(1f,0.5f,0,1f);
    public static Color switchBtn_Off = new Color(44f/255f,121f/255f,159f/255f,1f);
    public static Color switchBtn_On = new Color(66f/255f,237f/255f,248f/255f,1f);//00FFEF_2  B700FF_3  FF000D_4
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
