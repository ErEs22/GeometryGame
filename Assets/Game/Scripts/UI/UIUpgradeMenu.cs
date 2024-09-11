using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    private string path_Text_RefreshCost = "Btn_Refresh/Text_RefreshCost";
    private string path_Health_PropertyValue = "PlayerStatusInfo/Properties/Health/Text_PropertyValue";
    private string path_HPRegeneration_PropertyValue = "PlayerStatusInfo/Properties/HPRegeneration/Text_PropertyValue";
    private string path_StealHP_PropertyValue = "PlayerStatusInfo/Properties/StealHP/Text_PropertyValue";
    private string path_DamageMul_PropertyValue = "PlayerStatusInfo/Properties/DamageMul/Text_PropertyValue";
    private string path_AttackSpeed_PropertyValue = "PlayerStatusInfo/Properties/AttackSpeed/Text_PropertyValue";
    private string path_CriticalRate_PropertyValue = "PlayerStatusInfo/Properties/CriticalRate/Text_PropertyValue";
    private string path_AttackRange_PropertyValue = "PlayerStatusInfo/Properties/AttackRange/Text_PropertyValue";
    private string path_MoveSpeed_PropertyValue = "PlayerStatusInfo/Properties/MoveSpeed/Text_PropertyValue";
    //------------

    private Vector2[] upgradeItemStartPos = {new Vector2(220,-210),new Vector2(540,-396),new Vector2(860,-210),new Vector2(1180,-396)};
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
    private TextMeshProUGUI text_Btn_Refresh;
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
    private int refreshCoinCost = 2;

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
        text_Btn_Refresh = transform.Find(path_Text_RefreshCost).GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable() {
        btn_Refresh.onClick.AddListener(OnRefreshClick);
        EventManager.instance.onShowUpgradeRewardCount += UpgradeRewardCount;
        EventManager.instance.onUpgradeButtonClick += OnUpgradeButtonClick;
        EventManager.instance.onUpdatePlayerProperty += UpdatePlayerStatusPropertiesUI;
        GenerateUpgradeItems(4);
        //先计算刷新所需金币，之后再更新UI显示
        SetFirstRefreshCoinCost();
        UpdateBtnRefreshUI();
        InitPlayerProperties();
    }

    private void OnDisable()
    {
        btn_Refresh.onClick.RemoveAllListeners();
        EventManager.instance.onShowUpgradeRewardCount -= UpgradeRewardCount;
        EventManager.instance.onUpgradeButtonClick -= OnUpgradeButtonClick;
        EventManager.instance.onUpdatePlayerProperty -= UpdatePlayerStatusPropertiesUI;
        ClearAllChildTips();
    }

    private void InitPlayerProperties()
    {
        text_Health_PropertyValue.text = GameCoreData.PlayerProperties.maxHP.ToString();
        text_HPRegeneration_PropertyValue.text = GameCoreData.PlayerProperties.hpRegeneration.ToString();
        text_StealHP_PropertyValue.text = GameCoreData.PlayerProperties.lifeSteal.ToString() + "%";
        text_DamageMul_PropertyValue.text = GameCoreData.PlayerProperties.damageMul.ToString() + "%";
        text_AttackSpeed_PropertyValue.text = GameCoreData.PlayerProperties.attackSpeedMul.ToString() + "%";
        text_CriticalRate_PropertyValue.text = GameCoreData.PlayerProperties.criticalRate.ToString() + "%";
        text_AttackRange_PropertyValue.text = GameCoreData.PlayerProperties.attackRange.ToString();
        text_MoveSpeed_PropertyValue.text = GameCoreData.PlayerProperties.moveSpeed.ToString() + "%";
        //TODO加载玩家存档，当玩家有处在游戏中的的存档
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
        uiID = eUIID.UpgradeMenu;
    }

    private void OnRefreshClick()
    {
        if(GameCoreData.PlayerProperties.coin >= 20)
        {
            RefreshAllItems();
            RefreshCoinCost();
        }
    }

    private void RefreshCoinCost()
    {
        GameCoreData.PlayerProperties.CostCoin(refreshCoinCost);
        SetRefreshIncreaseCoinCost();
        EventManager.instance.OnUpdateCoinCount();
        UpdateBtnRefreshUI();
    }

    private void SetFirstRefreshCoinCost()
    {
        refreshCoinCost = LevelManager.currentLevel + Mathf.Clamp(EyreUtility.Round(0.5f * LevelManager.currentLevel),1,int.MaxValue);
    }

    private void SetRefreshIncreaseCoinCost()
    {
        refreshCoinCost += Mathf.Clamp(EyreUtility.Round(0.5f * LevelManager.currentLevel),1,int.MaxValue);
    }

    private void UpdateBtnRefreshUI()
    {
        if(GameCoreData.PlayerProperties.coin < refreshCoinCost)
        {
            text_Btn_Refresh.color = GameColor.text_Debuff;
        }
        else
        {
            text_Btn_Refresh.color = GameColor.text_Buff;
        }
        text_Btn_Refresh.text = refreshCoinCost.ToString();
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

    private async void GenerateUpgradeItems(int count)
    {
        if(upgradeItemsParent.childCount > 0)
        {
            for(int i = 0; i < upgradeItemsParent.childCount; i++)
            {
                RectTransform itemTrans = upgradeItemsParent.GetChild(i).GetComponent<RectTransform>();
                if((i + 1) % 2 == 0)
                {
                    itemTrans.DOAnchorPosY(upgradeItemStartPos[i].y - 300,0.3f).OnComplete(()=>
                    {
                        Destroy(itemTrans.gameObject);
                    });
                }
                if((i + 1) % 2 == 1)
                {
                    itemTrans.DOAnchorPosY(upgradeItemStartPos[i].y + 300,0.3f).OnComplete(()=>
                    {
                        Destroy(itemTrans.gameObject);
                    });
                }
            }
        }
        await UniTask.Delay(400);
        int[] randomNumArr = EyreUtility.GetRandomNumbersInBetween(0,itemDatas.Length - 1,count);
        for(int i = 0; i < count; i++)
        {
            UpgradeItem item = Instantiate(upgradeItemPrefab,upgradeItemsParent).GetComponent<UpgradeItem>();
            RectTransform itemTrans = item.GetComponent<RectTransform>();
            if((i + 1) % 2 == 0)
            {
                itemTrans.anchoredPosition = new Vector2(upgradeItemStartPos[i].x,upgradeItemStartPos[i].y - 300);
                Debug.Log(itemTrans.localPosition);
                itemTrans.DOAnchorPosY(upgradeItemStartPos[i].y,0.5f);
            }
            if((i + 1) % 2 == 1)
            {
                itemTrans.anchoredPosition = new Vector2(upgradeItemStartPos[i].x,upgradeItemStartPos[i].y + 300);
                Debug.Log(itemTrans.localPosition);
                itemTrans.DOAnchorPosY(upgradeItemStartPos[i].y,0.5f);
            }
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
            EventManager.instance.OnOpenUI(eUIID.ShopMenu);
        }
        else
        {
            RefreshAllItems();
        }
    }

    private void UpdatePlayerStatusPropertiesUI(ePlayerProperty playerProperty,int propertyValue)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP:
                healthPropertyValue = GameCoreData.PlayerProperties.maxHP;
                text_Health_PropertyValue.text = healthPropertyValue.ToString();
            break;
            case ePlayerProperty.HPRegeneration:
                hpRegenerationPropertyValue = GameCoreData.PlayerProperties.hpRegeneration;
                text_HPRegeneration_PropertyValue.text = hpRegenerationPropertyValue.ToString();
            break;
            case ePlayerProperty.lifeSteal:
                stealHPPropertyValue = GameCoreData.PlayerProperties.lifeSteal;
                text_StealHP_PropertyValue.text = stealHPPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.DamageMul:
                damageMulPropertyValue = GameCoreData.PlayerProperties.damageMul;
                text_DamageMul_PropertyValue.text = damageMulPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackSpeed:
                attackSpeedPropertyValue = GameCoreData.PlayerProperties.attackSpeedMul;
                text_AttackSpeed_PropertyValue.text = attackSpeedPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.CriticalRate:
                criticalRatePropertyValue = GameCoreData.PlayerProperties.criticalRate;
                text_CriticalRate_PropertyValue.text = criticalRatePropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackRange:
                attackRangePropertyValue = GameCoreData.PlayerProperties.attackRange;
                text_AttackRange_PropertyValue.text = attackRangePropertyValue.ToString();
            break;
            case ePlayerProperty.MoveSpeed:
                moveSpeedPropertyValue = GameCoreData.PlayerProperties.moveSpeed;
                text_MoveSpeed_PropertyValue.text = moveSpeedPropertyValue.ToString() + "%";
            break;
        }
    }
}