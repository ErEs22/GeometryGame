using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeMenu : UIBase
{
    //------------path
    private string path_Btn_TestConfirm = "Btn_TestConfirm";
    private string path_Text_UpgradeRewardCount = "Text_UpgradeRewardCount";
    //------------

    private Button btn_TestConfirm;
    private TextMeshProUGUI text_UpgradeRewardCount;

    private void Awake() {
        btn_TestConfirm = GameObject.Find(path_Btn_TestConfirm).GetComponentInParent<Button>();
        text_UpgradeRewardCount = GameObject.Find(path_Text_UpgradeRewardCount).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        btn_TestConfirm.onClick.AddListener(OnTestConfirmClick);
        EventManager.instance.onShowUpgradeRewardCount += UpgradeRewardCount;
    }

    private void OnDisable()
    {
        btn_TestConfirm.onClick.RemoveAllListeners();
        EventManager.instance.onShowUpgradeRewardCount -= UpgradeRewardCount;
    }

    public override void InitUI()
    {
        uiID = UIID.UpgradeMenu;
    }

    private void OnTestConfirmClick()
    {
        CloseUI();
        EventManager.instance.OnOpenUI(UIID.SkillMenu);
    }

    private void UpgradeRewardCount(int count)
    {
        text_UpgradeRewardCount.text = "UpgradeRewardCount:" + count;
    }
}