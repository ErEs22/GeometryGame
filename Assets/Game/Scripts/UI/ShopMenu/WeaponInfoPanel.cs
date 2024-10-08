using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : MonoBehaviour
{
    //-----------Path
    private const string path_Img_ItemIcon = "ItemInfo/Img_ItemIcon";
    private const string path_Text_ItemName = "ItemInfo/Text_ItemName";
    private const string path_Text_ItemRareLevel = "ItemInfo/Text_ItemRareLevel";
    private const string path_PropertiesParent = "ItemInfo/Properties";
    private const string path_Btn_Combine = "Btn_Combine";
    private const string path_Btn_Sell = "Btn_Sell";
    private const string path_Btn_Cancel = "Btn_Cancel";
    private const string path_Text_Sell = path_Btn_Sell + "/Text";
    //-----------

    private Image img_ItemIcon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemRareLevel;
    private TextMeshProUGUI text_Sell;
    private Button btn_Combine;
    private Button btn_Sell;
    private Button btn_Cancel;
    private Transform trans_PropertiesParent;
    public Item_Weapon activeItem;

    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemRareLevel = transform.Find(path_Text_ItemRareLevel).GetComponent<TextMeshProUGUI>();
        text_Sell = transform.Find(path_Text_Sell).GetComponent<TextMeshProUGUI>();
        btn_Combine = transform.Find(path_Btn_Combine).GetComponent<Button>();
        btn_Sell = transform.Find(path_Btn_Sell).GetComponent<Button>();
        btn_Cancel = transform.Find(path_Btn_Cancel).GetComponent<Button>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);
    }

    private void OnEnable() {
        btn_Combine.onClick.AddListener(HidePanel);
        btn_Sell.onClick.AddListener(HidePanel);
        btn_Cancel.onClick.AddListener(HidePanel);
        btn_Combine.onClick.AddListener(OnCombineBtnClick);
        btn_Sell.onClick.AddListener(OnSellBtnClick);
    }

    private void OnDisable()
    {
        btn_Combine.onClick.RemoveAllListeners();
        btn_Sell.onClick.RemoveAllListeners();
        btn_Cancel.onClick.RemoveAllListeners();
    }

    private void OnCombineBtnClick()
    {
        EventManager.instance.OnCombineWeaponItem(activeItem);
    }

    private void OnSellBtnClick()
    {
        EventManager.instance.OnSellWeaponInventoryItems(activeItem);
    }

    public void HidePanel()
    {
        EventManager.instance.OnHideShopMenuMask();
        transform.GetComponent<RectTransform>().DOAnchorPosY(-400,0.3f);
    }

    public void ShowPanel()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(0,0.3f);
    }

    public void DisplayPropInfo(ShopItemData_Weapon_SO itemData,int currentLevel)
    {
        ClearPropInfo();
        img_ItemIcon.sprite = itemData.itemIcon;
        text_ItemName.text = itemData.itemName;
        text_Sell.text = "回收(" + EyreUtility.Round(itemData.itemCost / itemData.itemLevel * currentLevel * 0.8f).ToString() + ")";
        SetItemRareLevelText(currentLevel);
        GameObject propertyObject = trans_PropertiesParent.GetChild(0).gameObject;
        SetPropertyText(propertyObject.GetComponent<TextMeshProUGUI>(),itemData.itemProperties[0].weaponProperty,itemData.itemProperties[0].propertyValue);
        for(int i = 1; i < itemData.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(propertyObject,trans_PropertiesParent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair data = itemData.itemProperties[i];
            SetPropertyText(textComp,data.weaponProperty,data.propertyValue);
        }
        transform.GetComponent<RectTransform>().DOAnchorPosY(0,0.3f);
    }

    public void ClearPropInfo()
    {
        for(int i = 1; i < trans_PropertiesParent.childCount; i++)
        {
            Destroy(trans_PropertiesParent.GetChild(i).gameObject);
        }
    }

    private void SetPropertyText(TextMeshProUGUI textComp,eWeaponProperty weaponProperty,float propertyValue)
    {
        propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(weaponProperty,propertyValue,activeItem.itemData.itemLevel,activeItem.itemLevel);
        switch(weaponProperty)
        {
            case eWeaponProperty.Damage:
                textComp.text = "伤害:" + EyreUtility.Round(propertyValue * (GameCoreData.PlayerProperties.damageMul * 0.01f + 1));
                break;
            case eWeaponProperty.CriticalMul:
                textComp.text = "暴击伤害:x" + propertyValue;
                break;
            case eWeaponProperty.FireInterval:
                textComp.text = "攻击间隔:" + (propertyValue / (GameCoreData.PlayerProperties.attackSpeedMul * 0.01f + 1)).ToString("F2");
                break;
            case eWeaponProperty.KnockBack:
                textComp.text = "击退:" + propertyValue;
                break;
            case eWeaponProperty.AttackRange:
                textComp.text = "范围:" + (propertyValue + GameCoreData.PlayerProperties.attackRange).ToString();
                break;
            case eWeaponProperty.LifeSteal:
                textComp.text = "生命吸取:" + ((propertyValue * 100)+ GameCoreData.PlayerProperties.lifeSteal).ToString() + "%";
                break;
            case eWeaponProperty.DamageThrough:
                textComp.text = "穿透:" + propertyValue;
                break;
            case eWeaponProperty.CriticalRate:
                textComp.text = "暴击率:" + ((propertyValue * 100) + GameCoreData.PlayerProperties.criticalRate).ToString() + "%";
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