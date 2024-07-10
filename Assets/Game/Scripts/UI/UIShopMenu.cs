using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopMenu : UIBase
{
    //------------UIComponentRelatePath
    string path_btn_StartLevel = "Btn_StartLevel";
    string path_btn_Refresh = "Shop/Btn_Refresh";
    string path_Items = "Shop/Items";
    string path_Text_CoinCount = "Shop/Text_CoinCount";
    string path_PropInventoryParent = "PropInventory/Scroll View/Viewport/Items";
    string path_WeaponInventoryParent = "WeaponInventory/Items";
    string path_PropInfoPanel = "PropInventory/ItemPropInfoPanel";
    string path_WeaponInfoPanel = "WeaponInventory/ItemWeaponInfoPanel";
    string path_Img_Mask = "WeaponInventory/Mask";
    private string path_Health_PropertyValue = "PlayerStatusInfo/Properties/Health/Text_PropertyValue";
    private string path_HPRegeneration_PropertyValue = "PlayerStatusInfo/Properties/HPRegeneration/Text_PropertyValue";
    private string path_StealHP_PropertyValue = "PlayerStatusInfo/Properties/StealHP/Text_PropertyValue";
    private string path_DamageMul_PropertyValue = "PlayerStatusInfo/Properties/DamageMul/Text_PropertyValue";
    private string path_AttackSpeed_PropertyValue = "PlayerStatusInfo/Properties/AttackSpeed/Text_PropertyValue";
    private string path_CriticalRate_PropertyValue = "PlayerStatusInfo/Properties/CriticalRate/Text_PropertyValue";
    private string path_AttackRange_PropertyValue = "PlayerStatusInfo/Properties/AttackRange/Text_PropertyValue";
    private string path_MoveSpeed_PropertyValue = "PlayerStatusInfo/Properties/MoveSpeed/Text_PropertyValue";
    //------------End
    private Button btn_StartLevel;
    private Button btn_Refresh;
    private Transform trans_ItemsParent;
    private Transform trans_PropInventoryParent;
    private Transform trans_WeaponInventoryParent;
    private PropInfoPanel propInfoPanel;
    private WeaponInfoPanel weaponInfoPanel;
    private Image img_Mask;
    private TextMeshProUGUI text_UpgradeRewardCount;
    private TextMeshProUGUI text_Health_PropertyValue;
    private TextMeshProUGUI text_HPRegeneration_PropertyValue;
    private TextMeshProUGUI text_StealHP_PropertyValue;
    private TextMeshProUGUI text_DamageMul_PropertyValue;
    private TextMeshProUGUI text_AttackSpeed_PropertyValue;
    private TextMeshProUGUI text_CriticalRate_PropertyValue;
    private TextMeshProUGUI text_AttackRange_PropertyValue;
    private TextMeshProUGUI text_MoveSpeed_PropertyValue;
    private TextMeshProUGUI text_Btn_Refresh;
    private int healthPropertyValue;
    private int hpRegenerationPropertyValue;
    private int stealHPPropertyValue;
    private int damageMulPropertyValue;
    private int attackSpeedPropertyValue;
    private int criticalRatePropertyValue;
    private int attackRangePropertyValue;
    private int moveSpeedPropertyValue;
    private int refreshCoinCost = 2;
    private TextMeshProUGUI text_CoinCount;
    [SerializeField]
    private GameObject prefab_ShopItem_Prop;
    [SerializeField]
    private GameObject prefab_ShopItem_Weapon;
    [SerializeField]
    private GameObject prefab_InventoryItem_Prop;
    [SerializeField]
    private GameObject prefab_InventoryItem_Weapon;
    [SerializeField]
    private List<ShopItemData_SO> allItemDatas = new List<ShopItemData_SO>();
    private List<ShopItem> allShopItems = new List<ShopItem>();
    private List<Item_Prop> allPropInventoryItems = new List<Item_Prop>();
    [SerializeField][DisplayOnly]
    private List<Item_Weapon> allWeaponInventoryItems = new List<Item_Weapon>();

    private void Awake()
    {
        btn_StartLevel = transform.Find(path_btn_StartLevel).GetComponent<Button>();
        btn_Refresh = transform.Find(path_btn_Refresh).GetComponent<Button>();
        text_CoinCount = transform.Find(path_Text_CoinCount).GetComponent<TextMeshProUGUI>();
        trans_ItemsParent = transform.Find(path_Items);
        trans_PropInventoryParent = transform.Find(path_PropInventoryParent);
        trans_WeaponInventoryParent = transform.Find(path_WeaponInventoryParent);
        propInfoPanel = transform.Find(path_PropInfoPanel).GetComponent<PropInfoPanel>();
        weaponInfoPanel = transform.Find(path_WeaponInfoPanel).GetComponent<WeaponInfoPanel>();
        img_Mask = transform.Find(path_Img_Mask).GetComponent<Image>();
        text_Health_PropertyValue = transform.Find(path_Health_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_HPRegeneration_PropertyValue = transform.Find(path_HPRegeneration_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_StealHP_PropertyValue = transform.Find(path_StealHP_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_DamageMul_PropertyValue = transform.Find(path_DamageMul_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackSpeed_PropertyValue = transform.Find(path_AttackSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_CriticalRate_PropertyValue = transform.Find(path_CriticalRate_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackRange_PropertyValue = transform.Find(path_AttackRange_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_MoveSpeed_PropertyValue = transform.Find(path_MoveSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_Btn_Refresh = btn_Refresh.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        btn_StartLevel.onClick.AddListener(OnStartLevelClick);
        btn_Refresh.onClick.AddListener(OnBtnRefreshClick);
        EventManager.instance.onUpdateCoinCount += UpdateCoinCountText;
        EventManager.instance.onUpdateCoinCount += UpdateAllItemsUIInfo;
        EventManager.instance.onAddShopItemToInventory += AddShopItemToInventory;
        EventManager.instance.onShowShopMenuMask += ShowMask;
        EventManager.instance.onHideShopMenuMask += HideMask;
        EventManager.instance.onCombineWeaponItem += CombineWeaponInventoryItems;
        EventManager.instance.onSellWeaponInventoryItems += SellWeaponInventoryItems;
        EventManager.instance.onUpdatePlayerProperty += UpdatePlayerStatusPropertiesUI;
        SetFirstRefreshCoinCost();
        InitPlayerProperties();
        UpdateWeaponInventory();
    }

    private void OnDisable()
    {
        btn_StartLevel.onClick.RemoveAllListeners();
        btn_Refresh.onClick.RemoveAllListeners();
        EventManager.instance.onUpdateCoinCount -= UpdateCoinCountText;
        EventManager.instance.onUpdateCoinCount -= UpdateAllItemsUIInfo;
        EventManager.instance.onAddShopItemToInventory -= AddShopItemToInventory;
        EventManager.instance.onShowShopMenuMask -= ShowMask;
        EventManager.instance.onHideShopMenuMask -= HideMask;
        EventManager.instance.onCombineWeaponItem -= CombineWeaponInventoryItems;
        EventManager.instance.onSellWeaponInventoryItems -= SellWeaponInventoryItems;
        EventManager.instance.onUpdatePlayerProperty -= UpdatePlayerStatusPropertiesUI;
    }

    private void UpdateWeaponInventory()
    {
        ClearWeaponInventory();
        GameInventory.Instance.inventoryWeapons.ForEach( weapon =>
        {
            Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
            itemWeapon.InitItemPropUI(weaponInfoPanel,weapon.weaponData,weapon.weaponLevel);
            allWeaponInventoryItems.Add(itemWeapon);
        });
    }

    private void ClearWeaponInventory()
    {
        if(allWeaponInventoryItems.Count <= 0) return;
        for(int i = 0; i < allWeaponInventoryItems.Count; i++)
        {
            Item_Weapon weapon = allWeaponInventoryItems[i];
            allWeaponInventoryItems.RemoveAt(i);
            Destroy(weapon.gameObject);
        }
    }

    private void InitPlayerProperties()
    {
        text_Health_PropertyValue.text = GameCoreData.PlayerProperties.maxHP.ToString();
        text_HPRegeneration_PropertyValue.text = GameCoreData.PlayerProperties.hpRegeneration.ToString();
        text_StealHP_PropertyValue.text = GameCoreData.PlayerProperties.lifeSteal.ToString() + "%";
        text_DamageMul_PropertyValue.text = GameCoreData.PlayerProperties.damageMul.ToString() + "%";
        text_AttackSpeed_PropertyValue.text = GameCoreData.PlayerProperties.attackSpeedMul.ToString() + "%";
        text_CriticalRate_PropertyValue.text = GameCoreData.PlayerProperties.criticalRate.ToString() + "%";
        text_AttackRange_PropertyValue.text = GameCoreData.PlayerProperties.attackRange.ToString();
        text_MoveSpeed_PropertyValue.text = GameCoreData.PlayerProperties.moveSpeed.ToString() + "%";
        //TODO加载玩家存档，当玩家有处在游戏中的的存档
    }

    private void CombineWeaponInventoryItems(Item_Weapon weaponItem)
    {
        for(int i = 0; i < allWeaponInventoryItems.Count; i++)
        {
            Item_Weapon item = allWeaponInventoryItems[i];
            //排除自身
            if(weaponItem == item) continue;
            if(weaponItem.itemData.itemName == item.itemData.itemName && weaponItem.itemLevel == item.itemLevel)
            {
                allWeaponInventoryItems.Remove(weaponItem);
                GameInventory.Instance.RemoveWeaponFromInventory(weaponItem.itemData.itemName);
                weaponItem.Combine();
                item.UpgradeItemLevel();
                return;
            }
        }
        Debug.Log("Fail to combine,not enough weapon in inventory");
    }

    private void SellWeaponInventoryItems(Item_Weapon weaponItem)
    {
        allWeaponInventoryItems.Remove(weaponItem);
        GameInventory.Instance.RemoveWeaponFromInventory(weaponItem.itemData.itemName);
        GameCoreData.PlayerProperties.coin += (int)(weaponItem.itemData.itemLevel * weaponItem.itemData.itemCost * 0.8);
        EventManager.instance.OnUpdateCoinCount();
        Destroy(weaponItem.gameObject);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        RefreshAllItems();
        UpdateCoinCountText();
    }

    private void OnBtnRefreshClick()
    {
        if(GameCoreData.PlayerProperties.coin >= refreshCoinCost)
        {
            RefreshAllItems();
            RefreshCoinCost();
        }
    }

    private void RefreshCoinCost()
    {
        GameCoreData.PlayerProperties.CostCoin(refreshCoinCost);
        SetRefreshIncreaseCoinCost();
        EventManager.instance.OnUpdateCoinCount();
        UpdateBtnRefreshUI();
    }

    private void SetFirstRefreshCoinCost()
    {
        refreshCoinCost = LevelManager.currentLevel + Mathf.Clamp((int)(0.5 * LevelManager.currentLevel),1,int.MaxValue);
        text_Btn_Refresh.text = "Refresh(" + refreshCoinCost.ToString() + ")";
    }

    private void SetRefreshIncreaseCoinCost()
    {
        refreshCoinCost += Mathf.Clamp((int)(0.5 * LevelManager.currentLevel),1,int.MaxValue);
        text_Btn_Refresh.text = "Refresh(" + refreshCoinCost.ToString() + ")";
    }

    private void UpdateBtnRefreshUI()
    {
        if(GameCoreData.PlayerProperties.coin < 20)
        {
            text_Btn_Refresh.color = Color.red;
        }
        else
        {
            text_Btn_Refresh.color = Color.black;
        }
    }

    private void ShowMask()
    {
        img_Mask.gameObject.SetActive(true);
    }

    private void HideMask()
    {
        img_Mask.gameObject.SetActive(false);
    }

    private void UpdateAllItemsUIInfo()
    {
        allShopItems.ForEach(item =>
        {
            item.UpdateUIInfo();
        });
    }

    private void AddShopItemToInventory(ShopItemData_SO itemData,ShopItem shopItem)
    {
        int coinCount = GameCoreData.PlayerProperties.coin;
        switch(itemData.shopItemType)
        {
            case eShopItemType.Prop:
                Item_Prop tempItemProp = CheckItemInPropInventory(itemData as ShopItemData_Prop_SO);
                //检查背包是否已经存在这个物品，存在直接加一数量，否则添加该物品到背包中
                if(tempItemProp != null)
                {
                    tempItemProp.IncreaseItemPropCount();
                    GameInventory.Instance.AddPropAmount(tempItemProp.itemData.itemName);
                }
                else
                {
                    Item_Prop itemProp = Instantiate(prefab_InventoryItem_Prop,trans_PropInventoryParent).GetComponent<Item_Prop>();
                    itemProp.InitItemPropUI(propInfoPanel,itemData,shopItem.itemLevel);
                    allPropInventoryItems.Add(itemProp);
                    Inventory_Prop newProp = new Inventory_Prop{
                        propData = itemData as ShopItemData_Prop_SO,
                        propAmount = 1,
                    };
                    GameInventory.Instance.AddPropToInventory(newProp);
                }
                if(coinCount >= itemData.itemCost)
                {
                    coinCount = Mathf.Clamp(coinCount - itemData.itemCost,0,int.MaxValue);
                    GameCoreData.PlayerProperties.coin = coinCount;
                }
                ShopItemData_Prop_SO itemPropData = itemData as ShopItemData_Prop_SO;
                itemPropData.itemProperties.ForEach( item =>
                {
                    EventManager.instance.OnUpdatePlayerProperty(item.playerProperty,item.changeAmount);
                });
                EventManager.instance.OnUpdateCoinCount();
                shopItem.img_HideMask.gameObject.SetActive(true);
                shopItem.isLocked = false;
            break;
            case eShopItemType.Weapon:
                if(allWeaponInventoryItems.Count >= 6)
                {
                    for(int i = 0; i < allWeaponInventoryItems.Count; i++)
                    {
                        Item_Weapon item = allWeaponInventoryItems[i];
                        if(itemData.itemName == item.itemData.itemName && itemData.itemLevel == item.itemLevel)
                        {
                            item.UpgradeItemLevel();
                            if(coinCount >= itemData.itemCost)
                            {
                                coinCount = Mathf.Clamp(coinCount - itemData.itemCost,0,int.MaxValue);
                                GameCoreData.PlayerProperties.coin = coinCount;
                            }
                            EventManager.instance.OnUpdateCoinCount();
                            shopItem.img_HideMask.gameObject.SetActive(true);
                            shopItem.isLocked = false;
                            break;
                        }
                        if(i == 6)
                        {
                            Debug.Log("The weapon inventory is full,you need to clear some space to purchase it");
                            return;
                        }
                    }
                }
                else
                {
                    if(coinCount >= itemData.itemCost)
                    {
                        coinCount = Mathf.Clamp(coinCount - itemData.itemCost,0,int.MaxValue);
                        GameCoreData.PlayerProperties.coin = coinCount;
                    }
                    EventManager.instance.OnUpdateCoinCount();
                    Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
                    itemWeapon.InitItemPropUI(weaponInfoPanel,itemData,shopItem.itemLevel);
                    allWeaponInventoryItems.Add(itemWeapon);
                    shopItem.img_HideMask.gameObject.SetActive(true);
                    shopItem.isLocked = false;
                    //游戏武器背包同步添加
                    Inventory_Weapon inventory_Weapon = new Inventory_Weapon{
                        weaponData = itemData as ShopItemData_Weapon_SO,
                        weaponLevel = shopItem.itemLevel,
                    };
                    GameInventory.Instance.AddWeaponToInventory(inventory_Weapon);
                }
            break;
        }
    }

    /// <summary>
    /// 检查道具背包中是否含有该物品，存在则返回该物品，否则返回空
    /// </summary>
    /// <param name="itemData">检查的物品</param>
    /// <returns></returns>
    private Item_Prop CheckItemInPropInventory(ShopItemData_Prop_SO itemData)
    {
        foreach (var item in allPropInventoryItems)
        {
            if(itemData.itemName == item.itemData.itemName)
            {
                return item;
            }
        }
        return null;
    }

    public override void InitUI()
    {
        uiID = eUIID.ShopMenu;
    }

    private void OnStartLevelClick()
    {
        EventManager.instance.OnStartLevel();
        CloseUI();
    }

    private void RefreshAllItems()
    {
        if(trans_ItemsParent.childCount <= 0)
        {
            //第一次生成四个物品
            for(int i = 0; i < 4; i++)
            {
                GenerateItem(allItemDatas[EyreUtility.GetRandomNumbersInBetween(0,allItemDatas.Count - 1,1)[0]]);
                if(allShopItems.Count >= 4) break;
            }
        }
        else
        {
            //刷新四个物品
            //销毁已有物品
            ClearShopItems();
            for(int i = 0; i < 4; i++)
            {
                if(allShopItems.Count >= 4) break;
                GenerateItem(allItemDatas[EyreUtility.GetRandomNumbersInBetween(0,allItemDatas.Count - 1,1)[0]]);
            }
        }
    }

    /// <summary>
    /// 清除商店物品，被锁定的无法被清除
    /// </summary>
    private void ClearShopItems()
    {
        for(int i = 0; i < allShopItems.Count; i++)
        {
            if(!allShopItems[i].isLocked)
            {
                GameObject item = allShopItems[i].gameObject;
                allShopItems.RemoveAt(i);
                Destroy(item.gameObject);
                i--;
            }
        }
    }

    private void UpdateCoinCountText()
    {
        text_CoinCount.text = GameCoreData.PlayerProperties.coin.ToString();
    }

    private void GenerateItem(ShopItemData_SO itemData)
    {
        switch(itemData.shopItemType)
        {
            case eShopItemType.Prop:
                ShopItem_Prop itemProp = Instantiate(prefab_ShopItem_Prop,trans_ItemsParent).GetComponent<ShopItem_Prop>();
                allShopItems.Add(itemProp);
                itemProp.itemData = itemData as ShopItemData_Prop_SO;
                itemProp.InitItemProperties(3);
                itemProp.UpdateUIInfo();
            break;
            case eShopItemType.Weapon:
                ShopItem_Weapon itemWeapon = Instantiate(prefab_ShopItem_Weapon,trans_ItemsParent).GetComponent<ShopItem_Weapon>();
                allShopItems.Add(itemWeapon);
                itemWeapon.itemData = itemData as ShopItemData_Weapon_SO;
                itemWeapon.InitItemProperties(4);
                itemWeapon.UpdateUIInfo();
            break;
        }
    }

    private bool CheckHasEnoughCoin(int coinCost)
    {
        if(GameCoreData.PlayerProperties.coin >= coinCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdatePlayerStatusPropertiesUI(ePlayerProperty playerProperty,int propertyValue)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP:
                healthPropertyValue = GameCoreData.PlayerProperties.maxHP;
                text_Health_PropertyValue.text = healthPropertyValue.ToString();
            break;
            case ePlayerProperty.HPRegeneration:
                hpRegenerationPropertyValue = GameCoreData.PlayerProperties.hpRegeneration;
                text_HPRegeneration_PropertyValue.text = hpRegenerationPropertyValue.ToString();
            break;
            case ePlayerProperty.lifeSteal:
                stealHPPropertyValue = GameCoreData.PlayerProperties.lifeSteal;
                text_StealHP_PropertyValue.text = stealHPPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.DamageMul:
                damageMulPropertyValue = GameCoreData.PlayerProperties.damageMul;
                text_DamageMul_PropertyValue.text = damageMulPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackSpeed:
                attackSpeedPropertyValue = GameCoreData.PlayerProperties.attackSpeedMul;
                text_AttackSpeed_PropertyValue.text = attackSpeedPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.CriticalRate:
                criticalRatePropertyValue = GameCoreData.PlayerProperties.criticalRate;
                text_CriticalRate_PropertyValue.text = criticalRatePropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackRange:
                attackRangePropertyValue = GameCoreData.PlayerProperties.attackRange;
                text_AttackRange_PropertyValue.text = attackRangePropertyValue.ToString();
            break;
            case ePlayerProperty.MoveSpeed:
                moveSpeedPropertyValue = GameCoreData.PlayerProperties.moveSpeed;
                text_MoveSpeed_PropertyValue.text = moveSpeedPropertyValue.ToString() + "%";
            break;
        }
    }
}



