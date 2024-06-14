using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem_Weapon : ShopItem
{
    public ShopItemData_Weapon_SO itemData;

    private void OnEnable() {
        btn_Purchase.onClick.AddListener(OnBtnPurchaseClick);
        btn_Lock.onClick.AddListener(OnBtnLockClick);
    }

    private void OnDisable()
    {
        btn_Purchase.onClick.RemoveAllListeners();
        btn_Lock.onClick.RemoveAllListeners();
        ClearItemProperties();
    }

    private void OnBtnPurchaseClick()
    {
        if(!isAffordable){
            Debug.Log("Not Enough Coin to Purchase it!");
            return;
        }
        EventManager.instance.OnAddShopItemToInventory(itemData,this);
    }

    private void OnBtnLockClick()
    {
        isLocked = !isLocked;
        Image img_Lock = btn_Lock.GetComponent<Image>();
        if(isLocked)
        {
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = Color.white;
            btn_Lock.colors = b;
        }
        else
        {
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = Color.grey;
            btn_Lock.colors = b;
        }
    }

    /// <summary>
    /// 更新物品UI的信息，包括属性，是否可购买
    /// </summary>
    /// <param name="isAffordable">能否购买</param>
    public override void UpdateUIInfo()
    {
        Image img_Lock = btn_Lock.GetComponent<Image>();
        if(isLocked)
        {
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = Color.white;
            btn_Lock.colors = b;
        }
        else
        {
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = Color.grey;
            btn_Lock.colors = b;
        }
        if(GameCoreData.PlayerData.coin >= itemData.itemCost)
        {
            isAffordable = true;
        }
        else
        {
            isAffordable = false;
        }
        TextMeshProUGUI text = btn_Purchase.GetComponentInChildren<TextMeshProUGUI>();
        if(isAffordable)
        {
            text.color = Color.black;
        }
        else
        {
            text.color = Color.red;
        }
        for(int i = 0; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = trans_PropertiesParent.GetChild(i).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
    }

    /// <summary>
    /// 初始化物品属性栏，生成对应数量的属性文本对象并设置对应数据
    /// </summary>
    public void InitItemProperties(int itemLevel)
    {
        this.itemLevel = itemLevel;
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        GameObject text_Property = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(text_Property.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].weaponProperty,itemData.itemProperties[0].propertyValue);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
        SetItemLevelFilterColor();
    }

    private void ClearItemProperties()
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
                textComp.text = "Damage:" + propertyValue * (GameCoreData.PlayerData.damageMul * 0.01f + 1);
            break;
            case WeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:" + propertyValue;
            break;
            case WeaponProperty.FireInterval:
                textComp.text = "FireInterval:" + (propertyValue / (GameCoreData.PlayerData.attackSpeedMul * 0.01f + 1)).ToString("F2");
            break;
            case WeaponProperty.PushBack:
                textComp.text = "PushBack:" + propertyValue;
            break;
            case WeaponProperty.AttackRange:
                textComp.text = "AttackRange:" + (propertyValue + GameCoreData.PlayerData.attackRange).ToString();
            break;
            case WeaponProperty.StealHP:
                textComp.text = "StealHP:" + (propertyValue + GameCoreData.PlayerData.stealHP).ToString();
            break;
            case WeaponProperty.DamageThrough:
                textComp.text = "DamageThrough:" + propertyValue;
            break;
        }
    }
}
