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
    //------------End
    private Button btn_StartLevel;
    private Button btn_Refresh;
    private Transform trans_ItemsParent;
    private Transform trans_PropInventoryParent;
    private Transform trans_WeaponInventoryParent;
    private TextMeshProUGUI text_CoinCount;
    [SerializeField]
    private GameObject prefab_ShopItem_Prop;
    [SerializeField]
    private GameObject prefab_ShopItem_Weapon;
    [SerializeField]
    private List<ShopItemData_SO> allItemDatas = new List<ShopItemData_SO>();
    private List<ShopItem> allItems = new List<ShopItem>();

    private void Awake()
    {
        btn_StartLevel = transform.Find(path_btn_StartLevel).GetComponent<Button>();
        btn_Refresh = transform.Find(path_btn_Refresh).GetComponent<Button>();
        text_CoinCount = transform.Find(path_Text_CoinCount).GetComponent<TextMeshProUGUI>();
        trans_ItemsParent = transform.Find(path_Items);
        trans_PropInventoryParent = transform.Find(path_PropInventoryParent);
        trans_WeaponInventoryParent = transform.Find(path_WeaponInventoryParent);
    }

    private void OnEnable()
    {
        btn_StartLevel.onClick.AddListener(OnStartLevelClick);
        btn_Refresh.onClick.AddListener(OnBtnRefreshClick);
        EventManager.instance.onUpdateCoinCount += UpdateCoinCountText;
        EventManager.instance.onUpdateCoinCount += UpdateAllItemsUIInfo;
    }

    private void OnDisable()
    {
        btn_StartLevel.onClick.RemoveAllListeners();
        btn_Refresh.onClick.RemoveAllListeners();
        EventManager.instance.onUpdateCoinCount -= UpdateCoinCountText;
        EventManager.instance.onUpdateCoinCount -= UpdateAllItemsUIInfo;
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

    private void UpdateAllItemsUIInfo()
    {
        allItems.ForEach(item =>
        {
            item.UpdateUIInfo();
        });
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
                if(allItems.Count >= 4) break;
            }
        }
        else
        {
            //刷新四个物品
            //销毁已有物品
            ClearShopItems();
            for(int i = 0; i < 4; i++)
            {
                if(allItems.Count >= 4) break;
                GenerateItem(allItemDatas[EyreUtility.GetRandomNumbersInBetween(0,allItemDatas.Count - 1,1)[0]]);
            }
        }
    }

    /// <summary>
    /// 清除商店物品，被锁定的无法被清除
    /// </summary>
    private void ClearShopItems()
    {
        for(int i = 0; i < allItems.Count; i++)
        {
            if(!allItems[i].isLocked)
            {
                GameObject item = allItems[i].gameObject;
                allItems.RemoveAt(i);
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
                allItems.Add(itemProp);
                itemProp.itemData = itemData as ShopItemData_Prop_SO;
                itemProp.InitItemProperties();
                itemProp.UpdateUIInfo();
            break;
            case ShopItemType.Weapon:
                ShopItem_Weapon itemWeapon = Instantiate(prefab_ShopItem_Weapon,trans_ItemsParent).GetComponent<ShopItem_Weapon>();
                allItems.Add(itemWeapon);
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



