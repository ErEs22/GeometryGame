using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropInfoPanel : MonoBehaviour
{
    //-----------Path
    private const string path_Img_ItemIcon = "Img_ItemIcon";
    private const string path_Text_ItemName = "Text_ItemName";
    private const string path_Text_ItemRareLevel = "Text_ItemRareLevel";
    private const string path_PropertiesParent = "Properties";
    //-----------

    private Image img_ItemIcon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemRareLevel;
    private Transform trans_PropertiesParent;
    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemRareLevel = transform.Find(path_Text_ItemRareLevel).GetComponent<TextMeshProUGUI>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);
    }

    public void HidePanel()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(-400,0.3f);
    }

    public void ShowPanel()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(0,0.3f);
    }

    public void DisplayPropInfo(ShopItemData_Prop_SO itemData,int currentLevel)
    {
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        GameObject propertyObject = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(propertyObject.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].playerProperty,itemData.itemProperties[0].changeAmount);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(propertyObject,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopPropPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.playerProperty,data.changeAmount);
        }
        SetItemRareLevelText(currentLevel);
        transform.GetComponent<RectTransform>().DOAnchorPosY(0,0.3f);
    }

    public void ClearPropInfo()
    {
        for(int i = 1; i < trans_PropertiesParent.childCount; i++)
        {
            Destroy(trans_PropertiesParent.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,ePlayerProperty playerProperty,float propertyValue)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP:
                textComp.text = "生命值:" + propertyValue;
            break;
            case ePlayerProperty.HPRegeneration:
                textComp.text = "生命恢复:" + propertyValue;
            break;
            case ePlayerProperty.lifeSteal:
                textComp.text = "生命吸取:" + propertyValue + "%";
            break;
            case ePlayerProperty.DamageMul:
                textComp.text = "伤害:" + propertyValue + "%";
            break;
            case ePlayerProperty.AttackSpeed:
                textComp.text = "攻击速度:" + propertyValue + "%";
            break;
            case ePlayerProperty.CriticalRate:
                textComp.text = "暴击率:" + propertyValue + "%";
            break;
            case ePlayerProperty.AttackRange:
                textComp.text = "范围:" + propertyValue;
            break;
            case ePlayerProperty.MoveSpeed:
                textComp.text = "移动速度:" + propertyValue + "%";
            break;
            case ePlayerProperty.PickUpRange:
                textComp.text = "拾取范围" + propertyValue + "%";
            break;
        }
    }

    protected void SetItemRareLevelText(int itemLevel)
    {
        switch (itemLevel)
        {
            case 1:
                text_ItemRareLevel.text = "普通";
                text_ItemRareLevel.color = GameColor.ShopItem_Level01;
                break;
            case 2:
                text_ItemRareLevel.text = "特殊";
                text_ItemRareLevel.color = GameColor.ShopItem_Level02;
                break;
            case 3:
                text_ItemRareLevel.text = "稀有";
                text_ItemRareLevel.color = GameColor.ShopItem_Level03;
                break;
            case 4:
                text_ItemRareLevel.text = "传说";
                text_ItemRareLevel.color = GameColor.ShopItem_Level04;
                break;
            case 5:
                text_ItemRareLevel.color = GameColor.ShopItem_Level05;
                break;
            default: break;
        }
    }
}