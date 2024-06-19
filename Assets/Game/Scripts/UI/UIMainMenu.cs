using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private const string path_Btn_Start = "Buttons/Btn_Start";
    private const string path_Btn_Setting = "Buttons/Btn_Setting";
    private const string path_Btn_Exit = "Buttons/Btn_Exit";
    private const string path_SettingPanel = "SettingPanel/";
    private const string path_SettingPanel_Dropdown_Resolution = path_SettingPanel + "Settings/Resolution/Dropdown";
    private TMP_Dropdown dropdown_Resolution;
    private Button btn_Start;
    private Button btn_Setting;
    private Button btn_Exit;
    private GameObject settingPanel;
    private TMP_Dropdown tMP_Dropdown;

    private void Awake() {
        btn_Start = transform.Find(path_Btn_Start).GetComponent<Button>();
        btn_Setting = transform.Find(path_Btn_Setting).GetComponent<Button>();
        btn_Exit = transform.Find(path_Btn_Exit).GetComponent<Button>();
        dropdown_Resolution = transform.Find(path_SettingPanel_Dropdown_Resolution).GetComponent<TMP_Dropdown>();
        settingPanel = transform.Find(path_SettingPanel).gameObject;
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

    private void InitSettingPanel()
    {
        //Resolution
        dropdown_Resolution.options.Clear();
        foreach (GameResolution resolution in Enum.GetValues(typeof(GameResolution)))
        {
            string resolutionStr = resolution.ToString().Remove(0,2);
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(resolutionStr);
            dropdown_Resolution.options.Add(optionData);
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