using System.Collections;
using System.Collections.Generic;
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
    //------------End
    private Button btn_StartLevel;
    private Button btn_Refresh;
    private Transform trans_ItemsParent;
    private Transform trans_PropInventoryParent;
    private Transform trans_WeaponInventoryParent;
    private PropInfoPanel propInfoPanel;
    private WeaponInfoPanel weaponInfoPanel;
    private Image img_Mask;
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
    }

    private void CombineWeaponInventoryItems(Item_Weapon weaponItem)
    {
        for(int i = 0; i < allWeaponInventoryItems.Count; i++)
        {
            Item_Weapon item = allWeaponInventoryItems[i];
            //排除自身
            if(weaponItem == item) continue;
            if(weaponItem.itemData.itemName == item.itemData.itemName && weaponItem.item_Level == item.item_Level)
            {
                allWeaponInventoryItems.Remove(weaponItem);
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
        GameCoreData.PlayerData.coin += (int)(weaponItem.itemData.itemLevel * weaponItem.itemData.itemCost * 0.8);
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
        RefreshAllItems();
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
        int coinCount = GameCoreData.PlayerData.coin;
        switch(itemData.shopItemType)
        {
            case ShopItemType.Prop:
                Item_Prop tempItemProp = CheckItemInPropInventory(itemData as ShopItemData_Prop_SO);
                //检查背包是否已经存在这个物品，存在直接加一数量，否则添加该物品到背包中
                if(tempItemProp != null)
                {
                    tempItemProp.IncreaseItemPropCount();
                }
                else
                {
                    if(coinCount >= itemData.itemCost)
                    {
                        coinCount = Mathf.Clamp(coinCount - itemData.itemCost,0,int.MaxValue);
                        GameCoreData.PlayerData.coin = coinCount;
                    }
                    EventManager.instance.OnUpdateCoinCount();
                    Item_Prop itemProp = Instantiate(prefab_InventoryItem_Prop,trans_PropInventoryParent).GetComponent<Item_Prop>();
                    itemProp.InitItemPropUI(propInfoPanel,itemData);
                    allPropInventoryItems.Add(itemProp);
                    shopItem.img_HideMask.gameObject.SetActive(true);
                    shopItem.isLocked = false;
                }
            break;
            case ShopItemType.Weapon:
                //TODO检查背包是否已满（最大放置6个）,同时检查是否可以与背包中的武器进行合成（背包已满情况下）
                if(allWeaponInventoryItems.Count >= 6)
                {
                    for(int i = 0; i < allWeaponInventoryItems.Count; i++)
                    {
                        Item_Weapon item = allWeaponInventoryItems[i];
                        if(itemData.itemName == item.itemData.itemName && itemData.itemLevel == item.item_Level)
                        {
                            item.UpgradeItemLevel();
                            if(coinCount >= itemData.itemCost)
                            {
                                coinCount = Mathf.Clamp(coinCount - itemData.itemCost,0,int.MaxValue);
                                GameCoreData.PlayerData.coin = coinCount;
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
                        GameCoreData.PlayerData.coin = coinCount;
                    }
                    EventManager.instance.OnUpdateCoinCount();
                    Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
                    itemWeapon.InitItemPropUI(weaponInfoPanel,itemData);
                    allWeaponInventoryItems.Add(itemWeapon);
                    shopItem.img_HideMask.gameObject.SetActive(true);
                    shopItem.isLocked = false;
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
        uiID = UIID.ShopMenu;
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
        text_CoinCount.text = GameCoreData.PlayerData.coin.ToString();
    }

    private void GenerateItem(ShopItemData_SO itemData)
    {
        switch(itemData.shopItemType)
        {
            case ShopItemType.Prop:
                ShopItem_Prop itemProp = Instantiate(prefab_ShopItem_Prop,trans_ItemsParent).GetComponent<ShopItem_Prop>();
                allShopItems.Add(itemProp);
                itemProp.itemData = itemData as ShopItemData_Prop_SO;
                itemProp.InitItemProperties();
                itemProp.UpdateUIInfo();
            break;
            case ShopItemType.Weapon:
                ShopItem_Weapon itemWeapon = Instantiate(prefab_ShopItem_Weapon,trans_ItemsParent).GetComponent<ShopItem_Weapon>();
                allShopItems.Add(itemWeapon);
                itemWeapon.itemData = itemData as ShopItemData_Weapon_SO;
                itemWeapon.InitItemProperties();
                itemWeapon.UpdateUIInfo();
            break;
        }
    }

    private bool CheckHasEnoughCoin(int coinCost)
    {
        if(GameCoreData.PlayerData.coin >= coinCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}



