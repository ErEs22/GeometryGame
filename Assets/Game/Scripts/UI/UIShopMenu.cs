using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopMenu : UIBase
{
    //------------UIComponentRelatePath
    string path_btn_StartLevel = "Btn_StartLevel";
    //------------End
    private Button btn_StartLevel;

    private void Awake() {
        btn_StartLevel = transform.Find(path_btn_StartLevel).GetComponent<Button>();
    }

    private void OnEnable() {
        btn_StartLevel.onClick.AddListener(OnStartLevelClick);
    }

    private void OnDisable()
    {
        btn_StartLevel.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        uiID = UIID.ShopMenu;
    }

    private void OnStartLevelClick()
    {
        EventManager.instance.OnStartLevel();
        CloseUI();
    }
}



