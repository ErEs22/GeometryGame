using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    //-----------Path
    private string path_Image_Icon = "Icon";
    private string path_Text_ItemName = "Text_ItemName";
    private string path_Text_ItemDescription = "Text_ItemDesc";
    private string path_Text_ItemLevelDisplay = "Text_LevelDisplay";
    private string path_Btn_Upgrade = "Btn_Upgrade";
    //-----------
    private Image icon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemDescription;
    private TextMeshProUGUI text_ItemLevelDisplay;
    private Button btn_Upgrade;
    private int itemLevel;
    private int valueAmount;
    private UpgradeItemData_SO itemData;

    private void Awake() {
        BindFields();
    }

    private void OnEnable() {
        btn_Upgrade.onClick.AddListener(OnUpgradeBtnClick);
    }

    private void OnDisable() {
        btn_Upgrade.onClick.RemoveAllListeners();
    }

    private void BindFields()
    {
        icon = transform.Find(path_Image_Icon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemDescription = transform.Find(path_Text_ItemDescription).GetComponent<TextMeshProUGUI>();
        text_ItemLevelDisplay = transform.Find(path_Text_ItemLevelDisplay).GetComponent<TextMeshProUGUI>();
        btn_Upgrade = transform.Find(path_Btn_Upgrade).GetComponent<Button>();
    }

    private void SetUpgradeItemLevelByChance()
    {
        float randomNum = Random.Range(0f,1f);
        if(randomNum <= 0.8f)
        {
            itemLevel = 1;
            text_ItemLevelDisplay.text = "1级";
            text_ItemLevelDisplay.color = GameColor.ShopItem_Level01;
        }
        else if(randomNum > 0.8f && randomNum <= 0.9f)
        {
            itemLevel = 2;
            text_ItemLevelDisplay.text = "2级";
            text_ItemLevelDisplay.color = GameColor.ShopItem_Level02;
        }
        else if(randomNum > 0.9f && randomNum <= 0.97f)
        {
            itemLevel = 3;
            text_ItemLevelDisplay.text = "3级";
            text_ItemLevelDisplay.color = GameColor.ShopItem_Level03;
        }
        else
        {
            itemLevel = 4;
            text_ItemLevelDisplay.text = "4级";
            text_ItemLevelDisplay.color = GameColor.ShopItem_Level04;
        }
        valueAmount = itemData.valueAmount * itemLevel;
    }

    public void Init(UpgradeItemData_SO itemData,int itemLevel)
    {
        this.itemData = itemData;
        icon.sprite = itemData.icon;
        text_ItemName.text = itemData.itemName + " " + itemLevel;
        this.itemLevel = itemLevel;
        string sign = itemData.valueAmount > 0 ? "∮" : "-";
        SetUpgradeItemLevelByChance();
        switch(itemData.effectProperty)
        {
            case ePlayerProperty.MaxHP:
                text_ItemDescription.text = sign + valueAmount + "生命值";
            break;
            case ePlayerProperty.HPRegeneration:
                text_ItemDescription.text = sign + valueAmount + "生命再生";
            break;
            case ePlayerProperty.lifeSteal:
                text_ItemDescription.text = sign + valueAmount + "%生命吸取";
            break;
            case ePlayerProperty.DamageMul:
                text_ItemDescription.text = sign + valueAmount + "%伤害";
            break;
            case ePlayerProperty.AttackSpeed:
                text_ItemDescription.text = sign + valueAmount + "%攻击速度";
            break;
            case ePlayerProperty.CriticalRate:
                text_ItemDescription.text = sign + valueAmount + "%暴击率";
            break;
            case ePlayerProperty.AttackRange:
                text_ItemDescription.text = sign + valueAmount + "范围";
            break;
            case ePlayerProperty.MoveSpeed:
                text_ItemDescription.text = sign + valueAmount + "%移动速度";
            break;
        }
    }

    private void OnUpgradeBtnClick()
    {
        EventManager.instance.OnUpdatePlayerProperty(itemData.effectProperty,itemData.valueAmount);
        EventManager.instance.OnUpgradeButtonClick();
    }
}
