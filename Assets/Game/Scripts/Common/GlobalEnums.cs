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
    SkillMenu,
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