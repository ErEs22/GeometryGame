using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIShopMenu : UIBase
{
    //------------UIComponentRelatePath
    string path_TextTitle = "Shop/Text_Title";
    string path_btn_StartLevel = "Btn_StartLevel";
    string path_btn_Refresh = "Shop/Btn_Refresh";
    string path_Items = "Shop/Items";
    string path_Text_CoinCount = "Shop/Text_CoinCount";
    string path_PropInventoryParent = "PropInventory/Items";
    string path_WeaponInventoryParent = "WeaponInventory/Items";
    string path_PropInfoPanel = "PropInventory/ItemPropInfoPanel";
    string path_WeaponInfoPanel = "WeaponInventory/ItemWeaponInfoPanel";
    string path_Img_Mask = "WeaponInventory/Mask";
    private string path_Text_RefreshCost = "Shop/Btn_Refresh/Text_RefreshCost";
    private string path_Health_PropertyValue = "PlayerStatusInfo/Properties/Health/Text_PropertyValue";
    private string path_HPRegeneration_PropertyValue = "PlayerStatusInfo/Properties/HPRegeneration/Text_PropertyValue";
    private string path_StealHP_PropertyValue = "PlayerStatusInfo/Properties/StealHP/Text_PropertyValue";
    private string path_DamageMul_PropertyValue = "PlayerStatusInfo/Properties/DamageMul/Text_PropertyValue";
    private string path_AttackSpeed_PropertyValue = "PlayerStatusInfo/Properties/AttackSpeed/Text_PropertyValue";
    private string path_CriticalRate_PropertyValue = "PlayerStatusInfo/Properties/CriticalRate/Text_PropertyValue";
    private string path_AttackRange_PropertyValue = "PlayerStatusInfo/Properties/AttackRange/Text_PropertyValue";
    private string path_MoveSpeed_PropertyValue = "PlayerStatusInfo/Properties/MoveSpeed/Text_PropertyValue";
    //------------End
    private Vector2[] shopItemStartPos = {new Vector2(220,-210),new Vector2(540,-396),new Vector2(860,-210),new Vector2(1180,-396)};
    private Vector2[] weaponInventoryItemPos = {
        new Vector2(82,46),
        new Vector2(82,-46),
        new Vector2(0,-93f),
        new Vector2(-82,-46),
        new Vector2(-82,46),
        new Vector2(0,93f)
        };
    [SerializeField]
    private bool[] weaponOccupation = {false,false,false,false,false,false};
    private Vector2[] propInventoryItemPos = {
        new Vector2(50,-50),
        new Vector2(150,-50),
        new Vector2(250,-50),
        new Vector2(350,-50),
        new Vector2(450,-50),
        new Vector2(0,-136),
        new Vector2(100,-136),
        new Vector2(200,-136),
        new Vector2(300,-136),
        new Vector2(400,-136),
        new Vector2(50,-222),
        new Vector2(150,-222),
        new Vector2(250,-222),
        new Vector2(350,-222),
        new Vector2(450,-222)
    };
    private Button btn_StartLevel;
    private Button btn_Refresh;
    private Transform trans_ItemsParent;
    private Transform trans_PropInventoryParent;
    private Transform trans_WeaponInventoryParent;
    private PropInfoPanel propInfoPanel;
    private WeaponInfoPanel weaponInfoPanel;
    private Image img_Mask;
    private TextMeshProUGUI text_Title;
    private TextMeshProUGUI text_UpgradeRewardCount;
    private TextMeshProUGUI text_Health_PropertyValue;
    private TextMeshProUGUI text_HPRegeneration_PropertyValue;
    private TextMeshProUGUI text_StealHP_PropertyValue;
    private TextMeshProUGUI text_DamageMul_PropertyValue;
    private TextMeshProUGUI text_AttackSpeed_PropertyValue;
    private TextMeshProUGUI text_CriticalRate_PropertyValue;
    private TextMeshProUGUI text_AttackRange_PropertyValue;
    private TextMeshProUGUI text_MoveSpeed_PropertyValue;
    private TextMeshProUGUI text_Btn_RefreshCost;
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
    /// <summary>
    /// 商店在售物品列表
    /// </summary>
    private List<ShopItem> allShopItems = new List<ShopItem>();
    private List<Item_Prop> allPropInventoryItems = new List<Item_Prop>();
    private List<Item_Weapon> allWeaponInventoryItems = new List<Item_Weapon>();
    private List<ShopItemData_SO> allTier1Items = new List<ShopItemData_SO>();
    private List<ShopItemData_SO> allTier2Items = new List<ShopItemData_SO>();
    private List<ShopItemData_SO> allTier3Items = new List<ShopItemData_SO>();
    private List<ShopItemData_SO> allTier4Items = new List<ShopItemData_SO>();

    private void Awake()
    {
        btn_StartLevel = transform.Find(path_btn_StartLevel).GetComponent<Button>();
        btn_Refresh = transform.Find(path_btn_Refresh).GetComponent<Button>();
        text_Title = transform.Find(path_TextTitle).GetComponent<TextMeshProUGUI>();
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
        text_Btn_RefreshCost = transform.Find(path_Text_RefreshCost).GetComponent<TextMeshProUGUI>();
        SortAllTierShopItems();
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
        EventManager.instance.onGameover += ClearInventoryItems;
        SetFirstRefreshCoinCost();
        InitPlayerProperties();
        UpdateWeaponInventory();
        UpdatePropInventory();
        UpdateLevelText();
        UpdateAllItemsUIInfo();
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
        EventManager.instance.onGameover -= ClearInventoryItems;
    }

    private void SortAllTierShopItems()
    {
        foreach (ShopItemData_SO item in allItemDatas)
        {
            switch(item.itemLevel)
            {
                case 1:
                    allTier1Items.Add(item);
                break;
                case 2:
                    allTier2Items.Add(item);
                break;
                case 3:
                    allTier3Items.Add(item);
                break;
                case 4:
                    allTier4Items.Add(item);
                break;
            }
        }
    }

    private ShopItemData_SO GetItemDataByChance()
    {
        float randomNum = Random.Range(0f,1f);
        if(randomNum <= 0.8f)
        {
            return allTier1Items[EyreUtility.GetRandomNumbersInBetween(0,allTier1Items.Count - 1,1)[0]];
        }
        else if(randomNum > 0.8f && randomNum <= 0.9f)
        {
            return allTier2Items[EyreUtility.GetRandomNumbersInBetween(0,allTier2Items.Count - 1,1)[0]];
        }
        else if(randomNum > 0.9f && randomNum <= 0.97f)
        {
            return allTier3Items[EyreUtility.GetRandomNumbersInBetween(0,allTier3Items.Count - 1,1)[0]];
        }
        else
        {
            return allTier4Items[EyreUtility.GetRandomNumbersInBetween(0,allTier4Items.Count - 1,1)[0]];
        }
    }

    private void UpdateLevelText()
    {
        text_Title.text = "商店(关卡" + LevelManager.currentLevel.ToString() + ")";
    }

    private void UpdateWeaponInventory()
    {
        ClearWeaponInventory();
        GameInventory.Instance.inventoryWeapons.ForEach( weapon =>
        {
            Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
            SetItemInventoryPos(itemWeapon);
            // itemWeapon.transform.localPosition = weaponInventoryItemPos[allWeaponInventoryItems.Count];
            itemWeapon.InitItemPropUI(weaponInfoPanel,weapon.weaponData,weapon.weaponLevel,weapon.sellPrice);
            allWeaponInventoryItems.Add(itemWeapon);
            itemWeapon.inventory_Weapon = weapon;
        });
    }

    private void ClearWeaponInventory()
    {
        if(allWeaponInventoryItems.Count <= 0) return;
        for(int i = 0; i < allWeaponInventoryItems.Count; i++)
        {
            Item_Weapon weapon = allWeaponInventoryItems[i];
            SetItemInventoryPosFree(weapon);
            allWeaponInventoryItems.RemoveAt(i);
            Destroy(weapon.gameObject);
            i--;
        }
    }

    private void UpdatePropInventory()
    {
        ClearPropInventory();
        GameInventory.Instance.inventoryProps.ForEach( prop =>
        {
            Item_Prop itemProp = Instantiate(prefab_InventoryItem_Prop,trans_PropInventoryParent).GetComponent<Item_Prop>();
            itemProp.GetComponent<RectTransform>().anchoredPosition = propInventoryItemPos[allPropInventoryItems.Count];
            itemProp.InitItemPropUI(propInfoPanel,prop.propData,prop.propLevel);
            allPropInventoryItems.Add(itemProp);
        });
    }

    private void ClearPropInventory()
    {
        if(allPropInventoryItems.Count <= 0) return;
        for(int i = 0; i < allPropInventoryItems.Count; i++)
        {
            Item_Prop prop = allPropInventoryItems[i];
            allPropInventoryItems.RemoveAt(i);
            Destroy(prop.gameObject);
            i--;
        }
    }

    private void ClearInventoryItems()
    {
        ClearPropInventory();
        ClearWeaponInventory();
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
    }

    private void CombineWeaponInventoryItems(Item_Weapon weaponItem)
    {
        //超过最高等级无法合成
        if(weaponItem.itemLevel >= 4) return;
        for(int i = 0; i < allWeaponInventoryItems.Count; i++)
        {
            Item_Weapon item = allWeaponInventoryItems[i];
            //排除自身
            if(weaponItem == item) continue;
            if(weaponItem.itemData.itemName == item.itemData.itemName && weaponItem.itemLevel == item.itemLevel)
            {
                SetItemInventoryPosFree(weaponItem);
                allWeaponInventoryItems.Remove(weaponItem);
                GameInventory.Instance.RemoveWeaponFromInventory(weaponItem.inventory_Weapon);
                weaponItem.Combine();
                item.UpgradeItemLevel();
                return;
            }
        }
        Debug.Log("Fail to combine,not enough weapon in inventory");
    }

    private void SellWeaponInventoryItems(Item_Weapon weaponItem)
    {
        SetItemInventoryPosFree(weaponItem);
        allWeaponInventoryItems.Remove(weaponItem);
        GameInventory.Instance.RemoveWeaponFromInventory(weaponItem.inventory_Weapon);
        GameCoreData.PlayerProperties.coin += EyreUtility.Round(weaponItem.itemData.itemCost / weaponItem.itemData.itemLevel * weaponItem.itemLevel * 0.8f);
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
        refreshCoinCost = LevelManager.currentLevel + Mathf.Clamp(EyreUtility.Round(0.5f * LevelManager.currentLevel),1,int.MaxValue);
        text_Btn_RefreshCost.text = refreshCoinCost.ToString();
    }

    private void SetRefreshIncreaseCoinCost()
    {
        refreshCoinCost += Mathf.Clamp(EyreUtility.Round(0.5f * LevelManager.currentLevel),1,int.MaxValue);
        text_Btn_RefreshCost.text = refreshCoinCost.ToString();
    }

    private void UpdateBtnRefreshUI()
    {
        if(GameCoreData.PlayerProperties.coin < refreshCoinCost)
        {
            text_Btn_RefreshCost.color = GameColor.text_Debuff;
        }
        else
        {
            text_Btn_RefreshCost.color = GameColor.text_Buff;
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
        UpdateBtnRefreshUI();
    }

    private void AddShopItemToInventory(ShopItemData_SO itemData,ShopItem shopItem)
    {
        int price = shopItem.price;
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
                    itemProp.GetComponent<RectTransform>().anchoredPosition = propInventoryItemPos[allPropInventoryItems.Count];
                    itemProp.InitItemPropUI(propInfoPanel,itemData,shopItem.itemLevel);
                    allPropInventoryItems.Add(itemProp);
                    Inventory_Prop newProp = new Inventory_Prop{
                        propData = itemData as ShopItemData_Prop_SO,
                        propAmount = 1,
                    };
                    GameInventory.Instance.AddPropToInventory(newProp);
                }
                if(coinCount >= price)
                {
                    coinCount = Mathf.Clamp(coinCount - price,0,int.MaxValue);
                    GameCoreData.PlayerProperties.coin = coinCount;
                }
                ShopItemData_Prop_SO itemPropData = itemData as ShopItemData_Prop_SO;
                itemPropData.itemProperties.ForEach( item =>
                {
                    EventManager.instance.OnUpdatePlayerProperty(item.playerProperty,item.changeAmount);
                });
                EventManager.instance.OnUpdateCoinCount();
                shopItem.SetItemInvisible();
                shopItem.isLocked = false;
            break;
            case eShopItemType.Weapon:
                if(allWeaponInventoryItems.Count >= 6)
                {
                    if(shopItem.itemLevel >= 4)
                    {
                        //超过最大等级，无法与背包中已有的武器进行合成
                        return;
                    }
                    //在背包中寻找与要购买的武器是否有同等级相同的武器进行合成
                    for(int i = 0; i < allWeaponInventoryItems.Count; i++)
                    {
                        Item_Weapon item = allWeaponInventoryItems[i];
                        if(itemData.itemName == item.itemData.itemName && shopItem.itemLevel == item.itemLevel)
                        {
                            item.UpgradeItemLevel();
                            if(coinCount >= price)
                            {
                                coinCount = Mathf.Clamp(coinCount - price,0,int.MaxValue);
                                GameCoreData.PlayerProperties.coin = coinCount;
                            }
                            EventManager.instance.OnUpdateCoinCount();
                            shopItem.SetItemInvisible();
                            shopItem.isLocked = false;
                            break;
                        }
                        if(i == 5)
                        {
                            //未找到可合成的武器，需要删除或合成背包中的武器才能继续购买
                            Debug.Log("The weapon inventory is full,you need to clear some space to purchase it");
                            return;
                        }
                    }
                }
                else
                {
                    if(coinCount >= price)
                    {
                        coinCount = Mathf.Clamp(coinCount - price,0,int.MaxValue);
                        GameCoreData.PlayerProperties.coin = coinCount;
                    }
                    EventManager.instance.OnUpdateCoinCount();
                    Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
                    itemWeapon.InitItemPropUI(weaponInfoPanel,itemData,shopItem.itemLevel,EyreUtility.Round(price * 0.8f));
                    SetItemInventoryPos(itemWeapon);
                    // itemWeapon.transform.localPosition = weaponInventoryItemPos[allWeaponInventoryItems.Count];
                    allWeaponInventoryItems.Add(itemWeapon);
                    shopItem.SetItemInvisible();
                    shopItem.isLocked = false;
                    //游戏武器背包同步添加
                    Inventory_Weapon inventory_Weapon = new Inventory_Weapon{
                        weaponData = itemData as ShopItemData_Weapon_SO,
                        weaponLevel = shopItem.itemLevel,
                        sellPrice = EyreUtility.Round(price * 0.8f),
                    };
                    itemWeapon.inventory_Weapon = inventory_Weapon;
                    GameInventory.Instance.AddWeaponToInventory(inventory_Weapon);
                }
            break;
        }
    }

    private void SetItemInventoryPos(Item_Weapon item)
    {
        for(int i = 0; i < 6; i++)
        {
            if(weaponOccupation[i] == false)
            {
                item.transform.localPosition = weaponInventoryItemPos[i];
                item.posIndex = i;
                weaponOccupation[i] = true;
                break;
            }
        }
    }

    private void SetItemInventoryPosFree(Item_Weapon item)
    {
        weaponOccupation[item.posIndex] = false;
        item.posIndex = -1;
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

    private async void RefreshAllItems()
    {
        if(trans_ItemsParent.childCount <= 0)
        {
            //第一次生成四个物品
            for(int i = 0; i < 4; i++)
            {
                RectTransform itemTrans = GenerateItem(GetItemDataByChance());
                if((i + 1) % 2 == 0)
                {
                    itemTrans.anchoredPosition = new Vector2(shopItemStartPos[i].x,shopItemStartPos[i].y - 300);
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y,0.5f);
                }
                if((i + 1) % 2 == 1)
                {
                    itemTrans.anchoredPosition = new Vector2(shopItemStartPos[i].x,shopItemStartPos[i].y + 300);
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y,0.5f);
                }
                if(allShopItems.Count >= 4) break;
            }
        }
        else
        {
            //确认商店目前是否空了
            bool isShopEmpty = true;
            for(int i = 0; i < 4; i++)
            {
                if(allShopItems[i].isPruchased == false)
                {
                    isShopEmpty = false;
                    break;
                }
            }
            //刷新四个物品
            //销毁已有物品
            ClearShopItems();
            //商店空了则取消刷新动画等待时间
            if(isShopEmpty == true)
            {
                await UniTask.Delay(100);
            }
            else
            {
                await UniTask.Delay(400);
            }
            //如果四个物品都已经被购买，则取消等待时间
            for(int i = 0; i < 4; i++)
            {
                if(allShopItems.Count >= 4) break;
                RectTransform itemTrans = GenerateItem(GetItemDataByChance());
                if((i + 1) % 2 == 0)
                {
                    itemTrans.anchoredPosition = new Vector2(shopItemStartPos[i].x,shopItemStartPos[i].y - 300);
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y,0.5f);
                }
                if((i + 1) % 2 == 1)
                {
                    itemTrans.anchoredPosition = new Vector2(shopItemStartPos[i].x,shopItemStartPos[i].y + 300);
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y,0.5f);
                }
            }
        }
    }

    /// <summary>
    /// 清除商店物品，被锁定的无法被清除
    /// </summary>
    private void ClearShopItems()
    {
        for(int i = 0; i < 4; i++)
        {
            if(!allShopItems[0].isLocked)
            {
                RectTransform itemTrans = allShopItems[0].GetComponent<RectTransform>();
                GameObject itemObject = allShopItems[0].gameObject;
                allShopItems.RemoveAt(0);
                if((i + 1) % 2 == 0)
                {
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y - 300,0.3f).OnComplete(()=>
                    {
                        Destroy(itemObject);
                    });
                }
                if((i + 1) % 2 == 1)
                {
                    itemTrans.DOAnchorPosY(shopItemStartPos[i].y + 300,0.3f).OnComplete(()=>
                    {
                        Destroy(itemObject);
                    });
                }
            }
        }
    }

    private void UpdateCoinCountText()
    {
        text_CoinCount.text = GameCoreData.PlayerProperties.coin.ToString();
    }

    private RectTransform GenerateItem(ShopItemData_SO itemData)
    {
        switch(itemData.shopItemType)
        {
            case eShopItemType.Prop:
                ShopItem_Prop itemProp = Instantiate(prefab_ShopItem_Prop,trans_ItemsParent).GetComponent<ShopItem_Prop>();
                allShopItems.Add(itemProp);
                itemProp.itemData = itemData as ShopItemData_Prop_SO;
                itemProp.InitItemProperties(itemProp.itemData.itemLevel);
                itemProp.UpdateUIInfo();
            return itemProp.GetComponent<RectTransform>();
            case eShopItemType.Weapon:
                ShopItem_Weapon itemWeapon = Instantiate(prefab_ShopItem_Weapon,trans_ItemsParent).GetComponent<ShopItem_Weapon>();
                allShopItems.Add(itemWeapon);
                itemWeapon.itemData = itemData as ShopItemData_Weapon_SO;
                itemWeapon.InitItemProperties(itemWeapon.itemData.itemLevel);
                itemWeapon.UpdateUIInfo();
            return itemWeapon.GetComponent<RectTransform>();
            default:
            return null;
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

    private int CaculateItemPrice(int itemBaseCost,int itemBaseLevel,int itemCurrentLevel)
    {
        int price = 0;
        price = itemBaseCost / itemBaseLevel * itemCurrentLevel;
        return price;
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



