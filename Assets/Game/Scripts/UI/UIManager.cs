using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private UIBase[] allUI;

    private void Awake() {
        allUI = GetComponentsInChildren<UIBase>(true);
    }

    private void Start() {
        InitUIWindow();
        OpenUI(eUIID.MainMenu);
        // OpenUI(UIID.UpgradeMenu);
        // OpenUI(UIID.PlayerStatusBar);
    }

    private void OnEnable() {
        EventManager.instance.onOpenUI += OpenUI;
        EventManager.instance.onCloseUI += CloseUI;
    }

    private void OnDisable() {
        EventManager.instance.onOpenUI -= OpenUI;
        EventManager.instance.onCloseUI -= CloseUI;
    }

    private void InitUIWindow()
    {
        //TODO 窗口有时错乱出现
        foreach (var ui in allUI)
        {
            ui.InitUI();
            ui.CloseUI();
        }
        EventManager.instance.OnOpenUI(eUIID.PlayerStatusBar);
        EventManager.instance.OnDisableUIInput();
    }

    private void OpenUI(eUIID id)
    {
        foreach (UIBase ui in allUI)
        {
            if(ui.uiID == id)
            {
                ui.OpenUI();
            }
        }
    }

    private void CloseUI(eUIID id)
    {
        foreach(UIBase ui in allUI)
        {
            if(ui.uiID == id)
            {
                ui.CloseUI();
            }
        }
    }
}
