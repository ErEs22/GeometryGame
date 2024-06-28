using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private const string path_Btn_Start = "Buttons/Btn_Start";
    private const string path_Btn_Setting = "Buttons/Btn_Setting";
    private const string path_Btn_Exit = "Buttons/Btn_Exit";
    private const string path_SettingPanel = "SettingPanel/";
    private const string path_SettingPanel_HorSelector_ScreenMode = path_SettingPanel + "Settings/ScreenMode/HorizontalSelector";
    private const string path_SettingPanel_Dropdown_Resolution = path_SettingPanel + "Settings/Resolution";
    private const string path_SettingPanel_SwitchButton_CameraShake = path_SettingPanel + "Settings/CameraShake";
    private const string path_SettingPanel_SwitchButton_DamageNumberDisplay = path_SettingPanel + "Settings/DamageNumberDisplay";
    private const string path_SettingPanel_HorSelector_FPSLimit = path_SettingPanel + "Settings/FPSLimit/HorizontalSelector";
    private const string path_SettingPanel_Slider_MainVolume = path_SettingPanel + "Settings/MainVolume";
    private const string path_SettingPanel_Slider_BackgroundVolume = path_SettingPanel + "Settings/BackgroundVolume";
    private const string path_SettingPanel_Slider_SoundEffectVolume = path_SettingPanel + "Settings/SoundEffectVolume";
    private Button btn_Start;
    private Button btn_Setting;
    private Button btn_Exit;
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
        horSelector_ScreenMode = transform.Find(path_SettingPanel_HorSelector_ScreenMode).GetComponent<HorizontalSelector>();
        dropdown_Resolution = transform.Find(path_SettingPanel_Dropdown_Resolution).GetComponent<TMP_Dropdown>();
        btnSwitch_CameraShake = transform.Find(path_SettingPanel_SwitchButton_CameraShake).GetComponent<SwitchButton>();
        btnSwitch_DamageNumberDisplay = transform.Find(path_SettingPanel_SwitchButton_DamageNumberDisplay).GetComponent<SwitchButton>();
        horSelector_FPSLimit = transform.Find(path_SettingPanel_HorSelector_FPSLimit).GetComponent<HorizontalSelector>();
        slider_MainVolume = transform.Find(path_SettingPanel_Slider_MainVolume).GetComponent<Slider>();
        slider_BackgroundVolume = transform.Find(path_SettingPanel_Slider_BackgroundVolume).GetComponent<Slider>();
        slider_SoundEffectVolume = transform.Find(path_SettingPanel_Slider_SoundEffectVolume).GetComponent<Slider>();
        settingPanel = transform.Find(path_SettingPanel).gameObject;
    }

    private void Start() {
    }

    private void OnEnable() {
        btn_Start.onClick.AddListener(OnBtnStartClick);
        btn_Setting.onClick.AddListener(OnBtnSettingClick);
        btn_Exit.onClick.AddListener(OnBtnExitClick);
    }

    private void OnDisable() {
        btn_Start.onClick.RemoveAllListeners();
        btn_Setting.onClick.RemoveAllListeners();
        btn_Setting.onClick.RemoveAllListeners();
        SaveGameSetting();
    }

    private void SaveGameSetting()
    {
        Enum.TryParse<eScreenMode>(horSelector_ScreenMode.selectOptions[horSelector_ScreenMode.selectIndex].optionName,out eScreenMode screenMode);
        GameCoreData.GameSetting.screenMode = screenMode;
        Enum.TryParse<eGameResolution>(dropdown_Resolution.options[dropdown_Resolution.value].text.Insert(0,"R_"),out eGameResolution gameResolution);
        GameCoreData.GameSetting.gameResolution = gameResolution;//TODO 修改字符串以符合枚举项
        GameCoreData.GameSetting.cameraShake = btnSwitch_CameraShake.isOn;
        GameCoreData.GameSetting.damageNumberDisplay = btnSwitch_DamageNumberDisplay.isOn;
        Enum.TryParse<eFPSOption>(horSelector_FPSLimit.selectOptions[horSelector_FPSLimit.selectIndex].optionName.Insert(0,"FPS_"),out eFPSOption fpsOption);
        GameCoreData.GameSetting.fpsLimit = fpsOption;
        GameCoreData.GameSetting.mainVolume = slider_MainVolume.value;
        GameCoreData.GameSetting.backgroundVolume = slider_BackgroundVolume.value;
        GameCoreData.GameSetting.soundEffectVolume = slider_SoundEffectVolume.value;
        GameCoreData.SaveData_GameSetting settingData = new GameCoreData.SaveData_GameSetting();
        SaveSystem.Save("GeometrySave.save",settingData);
    }

    private void LoadGameSetting()
    {
        SaveSystem.Load<GameCoreData.SaveData_GameSetting>("GeometrySave.save").LoadData();
        horSelector_ScreenMode.InitComponent(GameCoreData.GameSetting.screenMode.ToString());
        dropdown_Resolution.value = (int)GameCoreData.GameSetting.gameResolution;
        btnSwitch_CameraShake.SetButtonStatus(GameCoreData.GameSetting.cameraShake);
        btnSwitch_DamageNumberDisplay.SetButtonStatus(GameCoreData.GameSetting.damageNumberDisplay);
        horSelector_FPSLimit.InitComponent(GameCoreData.GameSetting.fpsLimit.ToString().Remove(0,4));
        slider_MainVolume.SetValueWithoutNotify(GameCoreData.GameSetting.mainVolume);
        slider_BackgroundVolume.SetValueWithoutNotify(GameCoreData.GameSetting.backgroundVolume);
        slider_SoundEffectVolume.SetValueWithoutNotify(GameCoreData.GameSetting.soundEffectVolume);
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

    private void OnBtnSettingClick()
    {
        settingPanel.SetActive(true);
        InitSettingPanel();
        LoadGameSetting();
    }

    private void OnBtnExitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}