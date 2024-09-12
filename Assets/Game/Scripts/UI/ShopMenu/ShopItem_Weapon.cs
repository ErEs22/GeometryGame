using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    }

    private void OnBtnPurchaseClick()
    {
        if(!isAffordable){
            Debug.Log("Not Enough Coin to Purchase it!");
            return;
        }
        EventManager.instance.OnAddShopItemToInventory(itemData,this);
        isPruchased = true;
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
            EventSystem.current.SetSelectedGameObject(null);
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = new Color(1,1,1,0);
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
            b.normalColor = new Color(1,1,1,0);
            btn_Lock.colors = b;
        }
        if(GameCoreData.PlayerProperties.coin >= price)
        {
            isAffordable = true;
        }
        else
        {
            isAffordable = false;
        }
        if(isAffordable)
        {
            text_Btn_Purchase.color = GameColor.text_Buff;
        }
        else
        {
            text_Btn_Purchase.color = GameColor.text_Debuff;
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
        isPruchased = false;
        this.itemLevel = itemLevel;
        CaculateItemPrice(itemData.itemCost,itemData.itemLevel,itemLevel);
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        text_Btn_Purchase.text = price.ToString();
        GameObject text_Property = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(text_Property.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].weaponProperty,itemData.itemProperties[0].propertyValue);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
        SetItemRareLevelText(itemLevel);
    }

    private void ClearItemProperties()
    {
        for(int i = 1; i < trans_PropertiesParent.childCount; i++)
        {
            Destroy(trans_PropertiesParent.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,eWeaponProperty weaponProperty,float propertyValue)
    {
        propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(weaponProperty,propertyValue,itemData.itemLevel,itemLevel);
        switch(weaponProperty)
        {
            case eWeaponProperty.Damage:
                textComp.text = "Damage:" + propertyValue * (GameCoreData.PlayerProperties.damageMul * 0.01f + 1);
                break;
            case eWeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:x" + propertyValue;
                break;
            case eWeaponProperty.FireInterval:
                textComp.text = "FireInterval:" + (propertyValue / (GameCoreData.PlayerProperties.attackSpeedMul * 0.01f + 1)).ToString("F2");
                break;
            case eWeaponProperty.KnockBack:
                textComp.text = "PushBack:" + propertyValue;
                break;
            case eWeaponProperty.AttackRange:
                textComp.text = "AttackRange:" + (propertyValue + GameCoreData.PlayerProperties.attackRange).ToString();
                break;
            case eWeaponProperty.LifeSteal:
                textComp.text = "LifeSteal:" + ((propertyValue * 100) + GameCoreData.PlayerProperties.lifeSteal).ToString() + "%";
                break;
            case eWeaponProperty.DamageThrough:
                textComp.text = "DamageThrough:" + propertyValue;
                break;
            case eWeaponProperty.CriticalRate:
                textComp.text = "CriticalRate:" + ((propertyValue * 100) + GameCoreData.PlayerProperties.criticalRate).ToString() + "%";
                break;
        }
    }
}
