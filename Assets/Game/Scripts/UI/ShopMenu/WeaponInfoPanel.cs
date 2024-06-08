using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : MonoBehaviour
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
    public void DisplayPropInfo(ShopItemData_Weapon_SO itemData)
    {
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        text_ItemType.text = itemData.itemType;
        GameObject propertyObject = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(propertyObject.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].weaponProperty,itemData.itemProperties[0].propertyValue);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(propertyObject,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
    }

    public void ClearPropInfo()
    {
        for(int i = 1; i < trans_PropertiesParent.childCount; i++)
        {
            Destroy(trans_PropertiesParent.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,WeaponProperty weaponProperty,float propertyValue)
    {
        switch(weaponProperty)
        {
            case WeaponProperty.Damage:
                textComp.text = "Damage:" + propertyValue * (PlayerPropertyHandler.damageMul * 0.01f + 1);
            break;
            case WeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:" + propertyValue;
            break;
            case WeaponProperty.FireInterval:
                textComp.text = "FireInterval:" + (propertyValue / (PlayerPropertyHandler.attackSpeed * 0.01f + 1)).ToString("F2");
            break;
            case WeaponProperty.PushBack:
                textComp.text = "PushBack:" + propertyValue;
            break;
            case WeaponProperty.AttackRange:
                textComp.text = "AttackRange:" + (propertyValue + PlayerPropertyHandler.attackRange).ToString();
            break;
            case WeaponProperty.StealHP:
                textComp.text = "StealHP:" + (propertyValue + PlayerPropertyHandler.stealHP).ToString();
            break;
            case WeaponProperty.DamageThrough:
                textComp.text = "DamageThrough:" + propertyValue;
            break;
        }
    }
}