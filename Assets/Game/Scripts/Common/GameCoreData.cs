using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameCoreData
{
    [Serializable]
    public class SaveData_GameSetting
    {
        public SaveData_GameSetting()
        {
            screenMode = GameSetting.screenMode;
            gameResolution = GameSetting.gameResolution;
            cameraShake = GameSetting.cameraShake;
            damageNumberDisplay = GameSetting.damageNumberDisplay;
            fpsLimit = GameSetting.fpsLimit;
            mainVolume = GameSetting.mainVolume;
            backgroundVolume = GameSetting.backgroundVolume;
            soundEffectVolume = GameSetting.soundEffectVolume;
        }

        public void LoadData()
        {
            GameSetting.screenMode = screenMode;
            GameSetting.gameResolution = gameResolution;
            GameSetting.cameraShake = cameraShake;
            GameSetting.damageNumberDisplay = damageNumberDisplay;
            GameSetting.fpsLimit = fpsLimit;
            GameSetting.mainVolume = mainVolume;
            GameSetting.backgroundVolume = backgroundVolume;
            GameSetting.soundEffectVolume = soundEffectVolume;
        }
        public eScreenMode screenMode;
        public eGameResolution gameResolution;
        public bool cameraShake;
        public bool damageNumberDisplay;
        public eFPSOption fpsLimit;
        public float mainVolume;
        public float backgroundVolume;
        public float soundEffectVolume;
    }
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

        public static eScreenMode screenMode = eScreenMode.FullScreen;
        public static eGameResolution gameResolution = eGameResolution.R_1920X1080;
        public static bool cameraShake = false;
        public static bool damageNumberDisplay = false;
        public static eFPSOption fpsLimit = eFPSOption.FPS_120;
        public static float mainVolume = 1f;
        public static float backgroundVolume = 1f;
        public static float soundEffectVolume = 1f;
    }
    public static class PlayerProperties
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
            EventManager.instance.OnUpdateCoinCount();
        }

        public static void GainCoin(int gain)
        {
            coin += gain;
            EventManager.instance.OnUpdateCoinCount();
        }
    }
}