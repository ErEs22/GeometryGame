using UnityEngine;

public class GameCoreData
{
    public static class GameSetting
    {
        //Setting Function
        //0.ScreenMode
        //1.Resolution
        //2.CameraShake
        //3.DamageNumberDisplay
        //4.FPS Limit
        //5.Audio
        //5.1.Main Volume
        //5.2.Background Volume
        //5.3.SoundEffect Volume

        public static ScreenMode screenMode = ScreenMode.FullScreen;
        public static GameResolution gameResolution = GameResolution.R_1920X1080;
    }
    public static class PlayerData
    {
        public static int coin = 0;
        public static int maxHP = 20;
        public static int hpRegeneration = 0;
        public static int stealHP = 0;
        public static int damageMul = 0;
        public static int attackSpeedMul = 0;
        public static int criticalRate = 0;
        public static int attackRange = 0;
        public static int moveSpeed = 0;
        public static int exp = 0;
        public static int bonusCoin = 0;
        public static int currentPlayerLevel = 1;

        public static void CostCoin(int cost)
        {
            coin -= cost;
        }

        public static void GainCoin(int gain)
        {
            coin += gain;
        }
    }
}