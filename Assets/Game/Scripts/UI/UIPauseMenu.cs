using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseMenu : UIBase
{
    private const string path_Btn_MainMenu = "Btn_MainMenu";
    private const string path_Btn_BackToGame = "Btn_BackToGame";
    private Button btn_MainMenu;
    private Button btn_BackToGame;

    private void Awake() {
        btn_MainMenu = transform.Find(path_Btn_MainMenu).GetComponent<Button>();
        btn_BackToGame = transform.Find(path_Btn_BackToGame).GetComponent<Button>();
    }

    private void OnEnable() {
        btn_MainMenu.onClick.AddListener(OnBtnMainMenuClick);
        btn_BackToGame.onClick.AddListener(OnBtnBackToGameClick);
        EventSystem.current.firstSelectedGameObject = null;
    }

    private void OnDisable() {
        btn_MainMenu.onClick.RemoveAllListeners();
        btn_BackToGame.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        base.InitUI();
        uiID = eUIID.PauseMenu;
    }

    private void OnBtnBackToGameClick()
    {
        Time.timeScale = 1;
        EventManager.instance.OnEnableUIInput();
        CloseUI();
    }

    private void OnBtnMainMenuClick()
    {
        Time.timeScale = 1;
        GlobalVar.gameStatus = eGameStatus.MainMenu;
        LevelManager.levelStatus = eLevelStatus.Ended;
        CloseUI();
        EventManager.instance.OnCloseUI(eUIID.ShopMenu);
        EventManager.instance.OnCloseUI(eUIID.UpgradeMenu);
        EventManager.instance.OnOpenUI(eUIID.MainMenu);
        EventManager.instance.OnDisableLocomotionInput();
        EventManager.instance.OnGameover();
        EventManager.instance.OnLevelEnd();
        GameCoreData.PlayerProperties.ResetPlayerProperties();
    }
}