using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private const string path_Btn_Start = "Buttons/Btn_Start";
    private const string path_Btn_Setting = "Buttons/Btn_Setting";
    private const string path_Btn_Exit = "Buttons/Btn_Exit";
    private const string path_SettingPanel = "SettingPanel";
    private Button btn_Start;
    private Button btn_Setting;
    private Button btn_Exit;
    private GameObject settingPanel;

    private void Awake() {
        btn_Start = transform.Find(path_Btn_Start).GetComponent<Button>();
        btn_Setting = transform.Find(path_Btn_Setting).GetComponent<Button>();
        btn_Exit = transform.Find(path_Btn_Exit).GetComponent<Button>();
        settingPanel = transform.Find(path_SettingPanel).gameObject;
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