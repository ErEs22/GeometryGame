using UnityEngine;
using UnityEngine.UI;

public class UIFinishMenu : UIBase
{
    //------------UIComponentRelatePath
    private string path_Btn_MainMenu = "Btn_MainMenu";
    //------------End
    private Button btn_MainMenu;

    private void Awake() {
        btn_MainMenu = transform.Find(path_Btn_MainMenu).GetComponent<Button>();
    }

    private void OnEnable() {
        btn_MainMenu.onClick.AddListener(OnMainMenuClick);
    }

    private void OnDisable() {
        btn_MainMenu.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        uiID = eUIID.FinishMenu;
    }

    private void OnMainMenuClick()
    {
        CloseUI();
        EventManager.instance.OnOpenUI(eUIID.MainMenu);
        GlobalVar.gameStatus = eGameStatus.MainMenu;
    }

}