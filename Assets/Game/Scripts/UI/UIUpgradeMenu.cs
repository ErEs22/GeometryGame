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
    private string path_Health_PropertyValue = "PlayerStatusInfo/Properties/Health/Text_PropertyValue";
    private string path_HPRegeneration_PropertyValue = "PlayerStatusInfo/Properties/HPRegeneration/Text_PropertyValue";
    private string path_StealHP_PropertyValue = "PlayerStatusInfo/Properties/StealHP/Text_PropertyValue";
    private string path_DamageMul_PropertyValue = "PlayerStatusInfo/Properties/DamageMul/Text_PropertyValue";
    private string path_AttackSpeed_PropertyValue = "PlayerStatusInfo/Properties/AttackSpeed/Text_PropertyValue";
    private string path_CriticalRate_PropertyValue = "PlayerStatusInfo/Properties/CriticalRate/Text_PropertyValue";
    private string path_AttackRange_PropertyValue = "PlayerStatusInfo/Properties/AttackRange/Text_PropertyValue";
    private string path_MoveSpeed_PropertyValue = "PlayerStatusInfo/Properties/MoveSpeed/Text_PropertyValue";
    //------------

    private Button btn_Refresh;
    private Transform upgradeItemsParent;
    private TextMeshProUGUI text_UpgradeRewardCount;
    private TextMeshProUGUI text_Health_PropertyValue;
    private TextMeshProUGUI text_HPRegeneration_PropertyValue;
    private TextMeshProUGUI text_StealHP_PropertyValue;
    private TextMeshProUGUI text_DamageMul_PropertyValue;
    private TextMeshProUGUI text_AttackSpeed_PropertyValue;
    private TextMeshProUGUI text_CriticalRate_PropertyValue;
    private TextMeshProUGUI text_AttackRange_PropertyValue;
    private TextMeshProUGUI text_MoveSpeed_PropertyValue;
    private GameObject upgradeCountTip;
    public GameObject upgradeTipPrefab;
    public GameObject upgradeItemPrefab;
    public UpgradeItemData_SO[] itemDatas;
    private int upgradeCount;
    private int healthPropertyValue;
    private int hpRegenerationPropertyValue;
    private int stealHPPropertyValue;
    private int damageMulPropertyValue;
    private int attackSpeedPropertyValue;
    private int criticalRatePropertyValue;
    private int attackRangePropertyValue;
    private int moveSpeedPropertyValue;

    private void Awake() {
        btn_Refresh = transform.Find(path_Btn_Refresh).GetComponentInParent<Button>();
        text_UpgradeRewardCount = transform.Find(path_Text_UpgradeRewardCount).GetComponent<TextMeshProUGUI>();
        upgradeCountTip = transform.Find(path_UpgradeCountTip).gameObject;
        upgradeItemsParent = transform.Find(path_UpgradeItems);
        text_Health_PropertyValue = transform.Find(path_Health_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_HPRegeneration_PropertyValue = transform.Find(path_HPRegeneration_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_StealHP_PropertyValue = transform.Find(path_StealHP_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_DamageMul_PropertyValue = transform.Find(path_DamageMul_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackSpeed_PropertyValue = transform.Find(path_AttackSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_CriticalRate_PropertyValue = transform.Find(path_CriticalRate_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackRange_PropertyValue = transform.Find(path_AttackRange_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_MoveSpeed_PropertyValue = transform.Find(path_MoveSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        btn_Refresh.onClick.AddListener(OnRefreshClick);
        EventManager.instance.onShowUpgradeRewardCount += UpgradeRewardCount;
        EventManager.instance.onUpgradeButtonClick += OnUpgradeButtonClick;
        EventManager.instance.onUpdatePlayerProperty += UpdatePlayerStatusPropertiesUI;
        GenerateUpgradeItems(4);
    }

    private void OnDisable()
    {
        btn_Refresh.onClick.RemoveAllListeners();
        EventManager.instance.onShowUpgradeRewardCount -= UpgradeRewardCount;
        EventManager.instance.onUpgradeButtonClick -= OnUpgradeButtonClick;
        EventManager.instance.onUpdatePlayerProperty -= UpdatePlayerStatusPropertiesUI;
        ClearAllChildTips();
    }

    private void ClearAllChildTips()
    {
        for(int i = 0; i < upgradeCountTip.transform.childCount; i++)
        {
            Destroy(upgradeCountTip.transform.GetChild(i).gameObject);
        }
    }

    private void ReduceTips(int reduceCount)
    {
        for(int i = 0; i < reduceCount; i++)
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
        upgradeCount = count;
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

    private void OnUpgradeButtonClick()
    {
        Mathf.Clamp(--upgradeCount,0,int.MaxValue);
        ReduceTips(1);
        if(upgradeCount <= 0)
        {
            CloseUI();
            EventManager.instance.OnOpenUI(UIID.SkillMenu);
        }
        else
        {
            RefreshAllItems();
        }
    }

    private void UpdatePlayerStatusPropertiesUI(PlayerProperty playerProperty,int propertyValue)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
                healthPropertyValue += propertyValue;
                text_Health_PropertyValue.text = healthPropertyValue.ToString();
            break;
            case PlayerProperty.HPRegeneration:
                hpRegenerationPropertyValue += propertyValue;
                text_HPRegeneration_PropertyValue.text = hpRegenerationPropertyValue.ToString();
            break;
            case PlayerProperty.StealHP:
                stealHPPropertyValue += propertyValue;
                text_StealHP_PropertyValue.text = stealHPPropertyValue.ToString() + "%";
            break;
            case PlayerProperty.DamageMul:
                damageMulPropertyValue += propertyValue;
                text_DamageMul_PropertyValue.text = damageMulPropertyValue.ToString() + "%";
            break;
            case PlayerProperty.AttackSpeed:
                attackSpeedPropertyValue += propertyValue;
                text_AttackSpeed_PropertyValue.text = attackSpeedPropertyValue.ToString() + "%";
            break;
            case PlayerProperty.CriticalRate:
                criticalRatePropertyValue += propertyValue;
                text_CriticalRate_PropertyValue.text = criticalRatePropertyValue.ToString() + "%";
            break;
            case PlayerProperty.AttackRange:
                attackRangePropertyValue += propertyValue;
                text_AttackRange_PropertyValue.text = attackRangePropertyValue.ToString();
            break;
            case PlayerProperty.MoveSpeed:
                moveSpeedPropertyValue += propertyValue;
                text_MoveSpeed_PropertyValue.text = moveSpeedPropertyValue.ToString() + "%";
            break;
        }
    }
}