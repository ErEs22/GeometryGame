using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem_Weapon : ShopItem
{
    //------------Path
    private const string path_Img_ItemIcon = "ItemInfo/Img_ItemIcon";
    private const string path_Text_ItemName = "ItemInfo/Text_ItemName";
    private const string path_Properties = "ItemInfo/Properties";
    //------------
    public ShopItemData_Weapon_SO itemData;
    private Image Img_ItemIcon;
    private TextMeshProUGUI text_ItemName;
    private Transform propertiesTrans;

    private void Awake() {
        Img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        propertiesTrans = transform.Find(path_Properties);
    }

    private void OnEnable() {
        InitItemProperties();
    }

    private void OnDisable()
    {
        ClearItemProperties();
    }

    private void InitItemProperties()
    {
        Img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        GameObject text_Property = propertiesTrans.GetChild(0).gameObject;
        SetPropertyText(text_Property.GetComponent<TextMeshProUGUI>(),itemData.ItemProperties[0].weaponProperty,itemData.ItemProperties[0].propertyValue);
        for(int i = 1; i < itemData.ItemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property,propertiesTrans).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.ItemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
    }

    private void ClearItemProperties()
    {
        for(int i = 1; i < propertiesTrans.childCount; i++)
        {
            Destroy(propertiesTrans.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,WeaponProperty weaponProperty,float propertyValue)
    {
        switch(weaponProperty)
        {
            case WeaponProperty.Damage:
                textComp.text = "Damage:" + propertyValue;
            break;
            case WeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:" + propertyValue;
            break;
            case WeaponProperty.FireInterval:
                textComp.text = "FireInterval" + propertyValue;
            break;
            case WeaponProperty.PushBack:
                textComp.text = "PushBack" + propertyValue;
            break;
            case WeaponProperty.AttackRange:
                textComp.text = "AttackRange" + propertyValue;
            break;
            case WeaponProperty.StealHP:
                textComp.text = "StealHP" + propertyValue;
            break;
            case WeaponProperty.DamageThrough:
                textComp.text = "DamageThrough" + propertyValue;
            break;
        }
    }
}
