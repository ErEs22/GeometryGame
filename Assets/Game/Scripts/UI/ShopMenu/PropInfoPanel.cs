using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropInfoPanel : MonoBehaviour
{
    //-----------Path
    private const string path_Img_ItemIcon = "Img_ItemIcon";
    private const string path_Text_ItemName = "Text_ItemName";
    private const string path_Text_ItemType = "Text_ItemType";
    private const string path_PropertiesParent = "Properties";
    //-----------

    private Image img_ItemIcon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemType;
    private Transform trans_PropertiesParent;
    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemType = transform.Find(path_Text_ItemType).GetComponent<TextMeshProUGUI>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);
    }

    public void DisplayPropInfo(ShopItemData_Prop_SO itemData)
    {
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        text_ItemType.text = itemData.itemType;
        GameObject propertyObject = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(propertyObject.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].playerProperty,itemData.itemProperties[0].changeAmount);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(propertyObject,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopPropPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.playerProperty,data.changeAmount);
        }
    }

    public void ClearPropInfo()
    {
        for(int i = 1; i < trans_PropertiesParent.childCount; i++)
        {
            Destroy(trans_PropertiesParent.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,PlayerProperty playerProperty,float propertyValue)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
                textComp.text = "MaxHP:" + propertyValue;
            break;
            case PlayerProperty.HPRegeneration:
                textComp.text = "HPRegeneration:" + propertyValue;
            break;
            case PlayerProperty.StealHP:
                textComp.text = "StealHP:" + propertyValue + "%";
            break;
            case PlayerProperty.DamageMul:
                textComp.text = "DamageMul:" + propertyValue + "%";
            break;
            case PlayerProperty.AttackSpeed:
                textComp.text = "AttackSpeed:" + propertyValue + "%";
            break;
            case PlayerProperty.CriticalRate:
                textComp.text = "CriticalRate:" + propertyValue + "%";
            break;
            case PlayerProperty.AttackRange:
                textComp.text = "AttackRange:" + propertyValue;
            break;
            case PlayerProperty.MoveSpeed:
                textComp.text = "MoveSpeed:" + propertyValue + "%";
            break;
        }
    }
}