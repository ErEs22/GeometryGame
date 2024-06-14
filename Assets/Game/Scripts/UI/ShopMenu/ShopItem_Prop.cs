using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem_Prop : ShopItem
{
    public ShopItemData_Prop_SO itemData;

    private void OnEnable() {
        btn_Purchase.onClick.AddListener(OnBtnPurchaseClick);
        btn_Lock.onClick.AddListener(OnBtnLockClick);
    }

    private void OnDisable() {
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
            // img_Lock.color = Color.white;
        }
        else
        {
            ColorBlock b = new ColorBlock();
            b = btn_Lock.colors;
            b.normalColor = Color.grey;
            btn_Lock.colors = b;
            // img_Lock.color = Color.grey;
        }
    }

    /// <summary>
    /// 更新物品UI的信息，包括属性，是否可购买，是否被锁定
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
    }

    /// <summary>
    /// 初始化物品属性栏，生成对应数量的属性文本对象并设置对应数据
    /// </summary>
    public void InitItemProperties(int itemLevel)
    {
        this.itemLevel = itemLevel;
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
        SetItemLevelFilterColor();
    }

    private void ClearItemProperties()
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
