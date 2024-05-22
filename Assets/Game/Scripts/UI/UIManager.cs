using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIBase[] allUI;

    private void Awake() {
        allUI = GetComponentsInChildren<UIBase>();
    }

    private void Start() {
        InitUIWindow();
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
        foreach (var ui in allUI)
        {
            ui.CloseUI();
        }
    }

    private void OpenUI(UIID id)
    {
        foreach (UIBase ui in allUI)
        {
            if(ui.uiID == id)
            {
                ui.OpenUI();
            }
        }
    }

    private void CloseUI(UIID id)
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
