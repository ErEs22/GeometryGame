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
    private const string path_SettingPanel_Dropdown_Resolution = path_SettingPanel + "Settings/Resolution/Dropdown";
    private const string path_SettingPanel_SwitchButton_CameraShake = path_SettingPanel + "Settings/CameraShake/Btn_Switch";
    private const string path_SettingPanel_SwitchButton_DamageNumberDisplay = path_SettingPanel + "Settings/DamageNumberDisplay/Btn_Switch";
    private const string path_SettingPanel_HorSelector_FPSLimit = path_SettingPanel + "Settings/FPSLimit/HorizontalSelector";
    private const string path_SettingPanel_Slider_MainVolume = path_SettingPanel + "Settings/MainVolume/Slider";
    private const string path_SettingPanel_Slider_BackgroundVolume = path_SettingPanel + "Settings/BackgroundVolume/Slider";
    private const string path_SettingPanel_Slider_SoundEffectVolume = path_SettingPanel + "Settings/SoundEffectVolume/Slider";
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
        InitSettingPanel();
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
    }

    private void SaveGameSetting()
    {
        Enum.TryParse<ScreenMode>(horSelector_ScreenMode.selectOptions[horSelector_ScreenMode.selectIndex].optionName,out ScreenMode screenMode);
        GameCoreData.GameSetting.screenMode = screenMode;
        Enum.TryParse<GameResolution>(dropdown_Resolution.options[dropdown_Resolution.value].text,out GameResolution gameResolution);
        GameCoreData.GameSetting.gameResolution = gameResolution;//TODO 修改字符串以符合枚举项
        GameCoreData.GameSetting.cameraShake = btnSwitch_CameraShake.isOn;
        GameCoreData.GameSetting.damageNumberDisplay = btnSwitch_DamageNumberDisplay.isOn;
        Enum.TryParse<FPSOption>(horSelector_FPSLimit.selectOptions[horSelector_FPSLimit.selectIndex].optionName.Insert(0,"FPS_"),out FPSOption fpsOption);
        GameCoreData.GameSetting.fpsLimit = fpsOption;
        GameCoreData.GameSetting.mainVolume = slider_MainVolume.value;
        GameCoreData.GameSetting.backgroundVolume = slider_BackgroundVolume.value;
        GameCoreData.GameSetting.soundEffectVolume = slider_SoundEffectVolume.value;
    }

    private void LoadGameSetting()
    {
        //TODO 加载设置
        horSelector_ScreenMode.InitComponent(GameCoreData.GameSetting.screenMode.ToString());
        // dropdown_Resolution.SetValueWithoutNotify()
    }

    private void InitSettingPanel()
    {
        //ScreenMode
        //Resolution
        dropdown_Resolution.options.Clear();
        dropdown_Resolution.onValueChanged.AddListener(OnDropdownResolutionValueChange);
        foreach (GameResolution resolution in Enum.GetValues(typeof(GameResolution)))
        {
            string resolutionStr = resolution.ToString().Remove(0,2);
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(resolutionStr);
            dropdown_Resolution.options.Add(optionData);
        }
        //CameraShake
        btnSwitch_CameraShake.SetButtonStatus(GameCoreData.GameSetting.cameraShake);
        //DamageNumberDisplay
        btnSwitch_DamageNumberDisplay.SetButtonStatus(GameCoreData.GameSetting.damageNumberDisplay);
        //Audio
          //Main
        slider_MainVolume.value = 1f;
          //Background
        slider_BackgroundVolume.value = 1f;
          //SoundEffect
        slider_SoundEffectVolume.value = 1f;
    }

    private void OnDropdownResolutionValueChange(int optionIndex)
    {
        var resolutionArray = Enum.GetValues(typeof(GameResolution));
        if(Enum.IsDefined(typeof(GameResolution),optionIndex))
        {
            GameCoreData.GameSetting.gameResolution = (GameResolution)Array.IndexOf(resolutionArray,optionIndex);
        }
        else
        {
            Debug.Log("The index of enum is out of the range");
        }
    }

    public override void InitUI()
    {
        uiID = UIID.MainMenu;
    }

    private void OnBtnStartClick()
    {
        EventManager.instance.OnOpenUI(UIID.CharacterSelectMenu);
        CloseUI();
    }

    private void OnBtnSettingClick()
    {
        settingPanel.SetActive(true);
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