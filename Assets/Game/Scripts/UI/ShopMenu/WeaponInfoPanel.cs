using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : MonoBehaviour
{
    //-----------Path
    private const string path_Img_ItemIcon = "ItemInfo/Img_ItemIcon";
    private const string path_Text_ItemName = "ItemInfo/Text_ItemName";
    private const string path_Text_ItemType = "ItemInfo/Text_ItemType";
    private const string path_PropertiesParent = "ItemInfo/Properties";
    private const string path_Btn_Combine = "Btn_Combine";
    private const string path_Btn_Sell = "Btn_Sell";
    private const string path_Btn_Cancel = "Btn_Cancel";
    //-----------

    private Image img_ItemIcon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemType;
    private Button btn_Combine;
    private Button btn_Sell;
    private Button btn_Cancel;
    private Transform trans_PropertiesParent;
    public Item_Weapon activeItem;

    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemType = transform.Find(path_Text_ItemType).GetComponent<TextMeshProUGUI>();
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

    private void HidePanel()
    {
        EventManager.instance.OnHideShopMenuMask();
        transform.position = new Vector3(-9999,-9999);
    }

    public void DisplayPropInfo(ShopItemData_Weapon_SO itemData)
    {
        ClearPropInfo();
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
        propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(weaponProperty,propertyValue,activeItem.itemData.itemLevel,activeItem.itemLevel);
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