using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeMenu : UIBase
{
    //------------path
    private string path_Btn_Refresh = "Btn_Refresh";
    private string path_Text_UpgradeRewardCount = "Text_UpgradeRewardCount";
    private string path_UpgradeCountTip = "UpgradeCountTip";
    private string path_UpgradeItems = "UpgradeItems";
    //------------

    private Button btn_Refresh;
    private Transform upgradeItemsParent;
    private TextMeshProUGUI text_UpgradeRewardCount;
    private GameObject upgradeCountTip;
    public GameObject upgradeTipPrefab;
    public GameObject upgradeItemPrefab;
    public UpgradeItemData_SO[] itemDatas;

    private void Awake() {
        btn_Refresh = transform.Find(path_Btn_Refresh).GetComponentInParent<Button>();
        text_UpgradeRewardCount = transform.Find(path_Text_UpgradeRewardCount).GetComponent<TextMeshProUGUI>();
        upgradeCountTip = transform.Find(path_UpgradeCountTip).gameObject;
        upgradeItemsParent = transform.Find(path_UpgradeItems).transform;
    }

    private void OnEnable() {
        btn_Refresh.onClick.AddListener(OnRefreshClick);
        EventManager.instance.onShowUpgradeRewardCount += UpgradeRewardCount;
        GenerateUpgradeItems(4);
    }

    private void OnDisable()
    {
        btn_Refresh.onClick.RemoveAllListeners();
        EventManager.instance.onShowUpgradeRewardCount -= UpgradeRewardCount;
        ClearAllChilds();
    }

    private void ClearAllChilds()
    {
        for(int i = 0; i < upgradeCountTip.transform.childCount; i++)
        {
            Destroy(upgradeCountTip.transform.GetChild(i).gameObject);
        }
    }

    public override void InitUI()
    {
        uiID = UIID.UpgradeMenu;
    }

    private void OnRefreshClick()
    {
        RefreshAllItems();
    }

    private void UpgradeRewardCount(int count)
    {
        text_UpgradeRewardCount.text = "UpgradeRewardCount:" + count;
        for(int i = 0; i < count; i++)
        {
            Instantiate(upgradeTipPrefab,upgradeCountTip.transform);
        }
    }

    private void GenerateUpgradeItems(int count)
    {
        if(upgradeItemsParent.childCount > 0)
        {
            for(int i = 0; i < upgradeItemsParent.childCount; i++)
            {
                Destroy(upgradeItemsParent.GetChild(i).gameObject);
            }
        }
        int[] randomNumArr = EyreUtility.GetRandomNumbersInBetween(0,itemDatas.Length - 1,count);
        for(int i = 0; i < count; i++)
        {
            UpgradeItem item = Instantiate(upgradeItemPrefab,upgradeItemsParent).GetComponent<UpgradeItem>();
            item.Init(itemDatas[randomNumArr[i]],1);
        }
    }

    private void RefreshAllItems()
    {
        GenerateUpgradeItems(4);
    }
}