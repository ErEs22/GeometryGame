using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private const string path_Btn_Start = "Buttons/Btn_Start";
    private const string path_Btn_Setting = "Buttons/Btn_Setting";
    private const string path_Btn_Exit = "Buttons/Btn_Exit";
    private const string path_SettingPanel = "SettingPanel/";
    private const string path_SettingPanel_Btn_Save = path_SettingPanel + "Btn_Save";
    private const string path_SettingPanel_HorSelector_ScreenMode = path_SettingPanel + "Settings/ScreenMode/HorizontalSelector";
    private const string path_SettingPanel_Dropdown_Resolution = path_SettingPanel + "Settings/Resolution";
    private const string path_SettingPanel_SwitchButton_DamageNumberDisplay = path_SettingPanel + "Settings/DamageNumberDisplay";
    private const string path_SettingPanel_HorSelector_FPSLimit = path_SettingPanel + "Settings/FPSLimit/HorizontalSelector";
    private const string path_SettingPanel_Slider_MainVolume = path_SettingPanel + "Settings/MainVolume";
    private const string path_SettingPanel_Slider_BackgroundVolume = path_SettingPanel + "Settings/BackgroundVolume";
    private const string path_SettingPanel_Slider_SoundEffectVolume = path_SettingPanel + "Settings/SoundEffectVolume";
    private Button btn_Start;
    private Button btn_Setting;
    private Button btn_Exit;
    private Button btn_Setting_Save;
    private HorizontalSelector horSelector_ScreenMode;
    private TMP_Dropdown dropdown_Resolution;
    private SwitchButton btnSwitch_CameraShake;
    private SwitchButton btnSwitch_DamageNumberDisplay;
    private HorizontalSelector horSelector_FPSLimit;
    private Slider slider_MainVolume;
    private Slider slider_BackgroundVolume;
    private Slider slider_SoundEffectVolume;
    private GameObject settingPanel;
    private TMP_Dropdown tMP_Dropdown;

    private void Awake() {
        btn_Start = transform.Find(path_Btn_Start).GetComponent<Button>();
        btn_Setting = transform.Find(path_Btn_Setting).GetComponent<Button>();
        btn_Exit = transform.Find(path_Btn_Exit).GetComponent<Button>();
        btn_Setting_Save = transform.Find(path_SettingPanel_Btn_Save).GetComponent<Button>();
        horSelector_ScreenMode = transform.Find(path_SettingPanel_HorSelector_ScreenMode).GetComponent<HorizontalSelector>();
        dropdown_Resolution = transform.Find(path_SettingPanel_Dropdown_Resolution).GetComponent<TMP_Dropdown>();
        btnSwitch_DamageNumberDisplay = transform.Find(path_SettingPanel_SwitchButton_DamageNumberDisplay).GetComponent<SwitchButton>();
        horSelector_FPSLimit = transform.Find(path_SettingPanel_HorSelector_FPSLimit).GetComponent<HorizontalSelector>();
        slider_MainVolume = transform.Find(path_SettingPanel_Slider_MainVolume).GetComponent<Slider>();
        slider_BackgroundVolume = transform.Find(path_SettingPanel_Slider_BackgroundVolume).GetComponent<Slider>();
        slider_SoundEffectVolume = transform.Find(path_SettingPanel_Slider_SoundEffectVolume).GetComponent<Slider>();
        settingPanel = transform.Find(path_SettingPanel).gameObject;
        LoadGameSetting(true);
        ApplyGameSetting();
        AudioManager.Instance.UpdateAudioInfo();
    }

    private void OnEnable() {
        btn_Start.onClick.AddListener(OnBtnStartClick);
        btn_Setting.onClick.AddListener(OnBtnSettingClick);
        btn_Exit.onClick.AddListener(OnBtnExitClick);
        btn_Setting_Save.onClick.AddListener(OnBtnSaveSettingClick);
        settingPanel.SetActive(false);
        AudioManager.Instance.PlayBGM(AudioManager.Instance.menuBGM);
    }

    private void OnDisable() {
        btn_Start.onClick.RemoveAllListeners();
        btn_Setting.onClick.RemoveAllListeners();
        btn_Setting.onClick.RemoveAllListeners();
        btn_Setting_Save.onClick.RemoveAllListeners();
    }

    private void SaveGameSetting()
    {
        GameCoreData.GameSetting.screenMode = GetScreenModeSetting();
        // Enum.TryParse<eGameResolution>(dropdown_Resolution.options[dropdown_Resolution.value].text.Insert(0,"R_"),out eGameResolution gameResolution);
        GameCoreData.GameSetting.gameResolution = GetGameResolutionSetting();
        GameCoreData.GameSetting.damageNumberDisplay = btnSwitch_DamageNumberDisplay.isOn;
        // Enum.TryParse<eFPSOption>(horSelector_FPSLimit.selectOptions[horSelector_FPSLimit.selectIndex].optionName.Insert(0,"FPS_"),out eFPSOption fpsOption);
        GameCoreData.GameSetting.fpsLimit = GetFPSLimitSetting();
        GameCoreData.GameSetting.mainVolume = slider_MainVolume.value;
        GameCoreData.GameSetting.backgroundVolume = slider_BackgroundVolume.value;
        GameCoreData.GameSetting.soundEffectVolume = slider_SoundEffectVolume.value;
        GameCoreData.SaveData_GameSetting settingData = new GameCoreData.SaveData_GameSetting();
        SaveSystem.Save("GeometrySave.save",settingData);
        AudioManager.Instance.UpdateAudioInfo();
    }

    private eScreenMode GetScreenModeSetting()
    {
        switch(horSelector_ScreenMode.selectIndex)
        {
            case 0:
                return eScreenMode.FullScreen;
            case 1:
                return eScreenMode.BorderlessWindow;
            case 2:
                return eScreenMode.Windowed;
            default:
                return eScreenMode.FullScreen;
        }
    }

    private void SetScreenModeSetting()
    {
        int optionIndex = 0;
        switch(GameCoreData.GameSetting.screenMode)
        {
            case eScreenMode.FullScreen:
                optionIndex = 0;
                break;
            case eScreenMode.BorderlessWindow:
                optionIndex = 1;
                break;
            case eScreenMode.Windowed:
                optionIndex = 2;
                break;
        }
        horSelector_ScreenMode.InitComponent(optionIndex);
    }

    private eFPSOption GetFPSLimitSetting()
    {
        switch(horSelector_FPSLimit.selectIndex)
        {
            case 0:
                return eFPSOption.FPS_30;
            case 1:
                return eFPSOption.FPS_60;
            case 2:
                return eFPSOption.FPS_120;
            case 3:
                return eFPSOption.FPS_UnLimited;
            default:
                return eFPSOption.FPS_UnLimited;
        }
    }

    private void SetFPSLimitSetting()
    {
        int optionIndex = 0;
        switch(GameCoreData.GameSetting.fpsLimit)
        {
            case eFPSOption.FPS_30:
                optionIndex = 0;
                break;
            case eFPSOption.FPS_60:
                optionIndex = 1;
                break;
            case eFPSOption.FPS_120:
                optionIndex = 2;
                break;
            case eFPSOption.FPS_UnLimited:
                optionIndex = 3;
                break;
        }
        horSelector_FPSLimit.InitComponent(optionIndex);
    }

    private eGameResolution GetGameResolutionSetting()
    {
        switch(dropdown_Resolution.value)
        {
            case 0:
                return eGameResolution.R_2560X1440;
            case 1:
                return eGameResolution.R_1920X1080;
            case 2:
                return eGameResolution.R_1600X900;
            case 3:
                return eGameResolution.R_1280X720;
            case 4:
                return eGameResolution.R_960X540;
            case 5:
                return eGameResolution.R_800X450;
            default:
                return eGameResolution.R_1920X1080;
        }
    }

    private void LoadGameSetting(bool audioOnly)
    {
        if (audioOnly)
        {
            GameCoreData.SaveData_GameSetting data = SaveSystem.Load<GameCoreData.SaveData_GameSetting>("GeometrySave.save");
            if(data != default)
            {
                data.LoadData();
            }
            else
            {
                GameCoreData.SaveData_GameSetting newSettingData = new GameCoreData.SaveData_GameSetting();
                newSettingData.screenMode = eScreenMode.FullScreen;
                newSettingData.fpsLimit = eFPSOption.FPS_UnLimited;
                newSettingData.gameResolution = eGameResolution.R_1920X1080;
                newSettingData.damageNumberDisplay = true;
                newSettingData.mainVolume = 1f;
                newSettingData.backgroundVolume = 1f;
                newSettingData.soundEffectVolume = 1f;
                newSettingData.LoadData();
                SaveSystem.Save("GeometrySave.save",newSettingData);
            }
        }
        else
        {
            GameCoreData.SaveData_GameSetting data = SaveSystem.Load<GameCoreData.SaveData_GameSetting>("GeometrySave.save");
            if(data != default)
            {
                data.LoadData();
            }
            else
            {
                return;
            }
            SetScreenModeSetting();
            dropdown_Resolution.value = (int)GameCoreData.GameSetting.gameResolution;
            btnSwitch_DamageNumberDisplay.SetButtonStatus(GameCoreData.GameSetting.damageNumberDisplay);
            SetFPSLimitSetting();
            slider_MainVolume.SetValueWithoutNotify(GameCoreData.GameSetting.mainVolume);
            slider_BackgroundVolume.SetValueWithoutNotify(GameCoreData.GameSetting.backgroundVolume);
            slider_SoundEffectVolume.SetValueWithoutNotify(GameCoreData.GameSetting.soundEffectVolume);
        }
    }

    private void InitSettingPanel()
    {
        //分辨率下拉框添加分辨率选项
        dropdown_Resolution.options.Clear();
        dropdown_Resolution.onValueChanged.AddListener(OnDropdownResolutionValueChange);
        foreach (eGameResolution resolution in Enum.GetValues(typeof(eGameResolution)))
        {
            string resolutionStr = resolution.ToString().Remove(0,2);
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(resolutionStr);
            dropdown_Resolution.options.Add(optionData);
        }
    }

    private void OnDropdownResolutionValueChange(int optionIndex)
    {
        var resolutionArray = Enum.GetValues(typeof(eGameResolution));
        if(Enum.IsDefined(typeof(eGameResolution),optionIndex))
        {
            GameCoreData.GameSetting.gameResolution = (eGameResolution)Array.IndexOf(resolutionArray,optionIndex);
        }
        else
        {
            Debug.Log("The index of enum is out of the range");
        }
    }

    public override void InitUI()
    {
        uiID = eUIID.MainMenu;
    }

    private void OnBtnStartClick()
    {
        EventManager.instance.OnOpenUI(eUIID.CharacterSelectMenu);
        CloseUI();
    }

    private void OnBtnSaveSettingClick()
    {
        SaveGameSetting();
        ApplyGameSetting();
    }

    private void OnBtnSettingClick()
    {
        settingPanel.SetActive(true);
        InitSettingPanel();
        LoadGameSetting(false);
    }

    private void OnBtnExitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void SetScreenResolution(eGameResolution resolution)
    {
        bool isFullScreen = GameCoreData.GameSetting.screenMode == eScreenMode.Windowed ? false : true;
        switch (resolution)
        {
            case eGameResolution.R_2560X1440:
                Screen.SetResolution(2560,1440,isFullScreen);
            break;
            case eGameResolution.R_1920X1080:
                Screen.SetResolution(1920,1080,isFullScreen);
            break;
            case eGameResolution.R_1600X900:
                Screen.SetResolution(1600,900,isFullScreen);
            break;
            case eGameResolution.R_1280X720:
                Screen.SetResolution(1280,720,isFullScreen);
            break;
            case eGameResolution.R_960X540:
                Screen.SetResolution(960,540,isFullScreen);
            break;
            case eGameResolution.R_800X450:
                Screen.SetResolution(800,450,isFullScreen);
            break;
        }
    }

    private void SetScreenMode(eScreenMode screenMode)
    {
        switch (screenMode)
        {
            case eScreenMode.Windowed:
            Screen.fullScreenMode = FullScreenMode.Windowed;
            break;
            case eScreenMode.BorderlessWindow:
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            break;
            case eScreenMode.FullScreen:
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            break;
        }
    }

    private void SetGameFPS(eFPSOption fPSOption)
    {
        switch(fPSOption)
        {
            case eFPSOption.FPS_30:
            Application.targetFrameRate = 30;
            break;
            case eFPSOption.FPS_60:
            Application.targetFrameRate = 60;
            break;
            case eFPSOption.FPS_120:
            Application.targetFrameRate = 120;
            break;
            case eFPSOption.FPS_UnLimited:
            Application.targetFrameRate = -1;
            break;
        }
    }

    private void ApplyGameSetting()
    {
        SetScreenResolution(GameCoreData.GameSetting.gameResolution);
        SetScreenMode(GameCoreData.GameSetting.screenMode);
        SetGameFPS(GameCoreData.GameSetting.fpsLimit);
    }
}