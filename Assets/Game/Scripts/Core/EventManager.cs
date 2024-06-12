using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event UnityAction<Enemy> enemyDie;
    public event UnityAction<int,int,int> onUICountDown = delegate{};
    public event UnityAction<UIID> onOpenUI = delegate{};
    public event UnityAction<UIID> onCloseUI = delegate{};
    public event UnityAction onStartLevel = delegate{};
    public event UnityAction<int,int> onUpdateHealthBar = delegate{};
    public event UnityAction<int,int,int> onUpdateExpBar = delegate{};
    public event UnityAction onUpdateCoinCount = delegate{};
    public event UnityAction<int> onUpdateBonusCoinCount = delegate{};
    public event UnityAction<int> onInitStatusBar = delegate{};
    public event UnityAction onInitPlayerStatus = delegate{};
    public event UnityAction<Color,Color> onHealthBarFlash = delegate{};
    public event UnityAction<Color,Color> onExpBarFlash = delegate{};
    public event UnityAction onCollectExpBall = delegate{};
    public event UnityAction<int> onShowUpgradeRewardCount = delegate{};
    public event UnityAction onPlayerUpgradeCountIncrease = delegate{};
    public event UnityAction<PlayerProperty,int> onUpdatePlayerProperty = delegate{};
    public event UnityAction<int> onChangeBonusCoinCount = delegate{};
    public event UnityAction onLevelEnd = delegate{};
    public event UnityAction onUpgradeButtonClick = delegate{};
    public event UnityAction<int> onShopItemPurchase = delegate{};
    public event UnityAction<ShopItemData_SO,ShopItem> onAddShopItemToInventory = delegate{};
    public event UnityAction onShowShopMenuMask = delegate{};
    public event UnityAction onHideShopMenuMask = delegate{};
    public event UnityAction<Item_Weapon> onCombineWeaponItem = delegate{};
    public event UnityAction<Item_Weapon> onSellWeaponInventoryItems = delegate{};
    public event UnityAction<CharacterData_SO> onUpdateSelectCharacterInfo = delegate{};
    public event UnityAction onStartGame = delegate{};

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OnStartGame()
    {
        onStartGame.Invoke();
    }

    public void OnUpdateSelectCharacterInfo(CharacterData_SO data)
    {
        onUpdateSelectCharacterInfo.Invoke(data);
    }

    public void OnUICountDown(int start,int end,int interval)
    {
        onUICountDown.Invoke(start,end,interval);
    }

    public void OnOpenUI(UIID id)
    {
        onOpenUI.Invoke(id);
    }

    public void OnCloseUI(UIID id)
    {
        onCloseUI.Invoke(id);
    }

    public void OnStartLevel()
    {
        onStartLevel.Invoke();
    }

    public void OnUpdateHealthBar(int currentHP,int maxHP)
    {
        onUpdateHealthBar.Invoke(currentHP,maxHP);
    }

    public void OnUpdateExpBar(int currentPlayerLevel,int currentExp,int currentLevelMaxExp)
    {
        onUpdateExpBar.Invoke(currentPlayerLevel,currentExp,currentLevelMaxExp);
    }

    public void OnUpdateCoinCount()
    {
        onUpdateCoinCount.Invoke();
    }

    public void OnUpdateBonusCoinCount(int bonusCoinCount)
    {
        onUpdateBonusCoinCount.Invoke(bonusCoinCount);
    }

    public void OnInitStatusBar(int maxHP)
    {
        onInitStatusBar.Invoke(maxHP);
    }

    public void OnInitPlayerStatus()
    {
        onInitPlayerStatus.Invoke();
    }

    public void OnHealthBarFlash(Color startColor,Color endColor)
    {
        onHealthBarFlash.Invoke(startColor,endColor);
    }

    public void OnExpBarFlash(Color startColor,Color endColor)
    {
        onExpBarFlash.Invoke(startColor,endColor);
    }

    public void OnCollectExpBall()
    {
        onCollectExpBall.Invoke();
    }

    public void OnShowUpgradeRewardCount(int count)
    {
        onShowUpgradeRewardCount.Invoke(count);
    }

    public void OnPlayerUpgradeCountIncrease()
    {
        onPlayerUpgradeCountIncrease.Invoke();
    }

    public void OnUpdatePlayerProperty(PlayerProperty playerProperty,int changeValue)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
                GameCoreData.PlayerData.maxHP += changeValue;
            break;
            case PlayerProperty.HPRegeneration:
                GameCoreData.PlayerData.hpRegeneration += changeValue;
            break;
            case PlayerProperty.StealHP:
                GameCoreData.PlayerData.stealHP += changeValue;
            break;
            case PlayerProperty.DamageMul:
                GameCoreData.PlayerData.damageMul += changeValue;
            break;
            case PlayerProperty.AttackSpeed:
                GameCoreData.PlayerData.attackSpeedMul += changeValue;
            break;
            case PlayerProperty.CriticalRate:
                GameCoreData.PlayerData.criticalRate += changeValue;
            break;
            case PlayerProperty.AttackRange:
                GameCoreData.PlayerData.attackRange += changeValue;
            break;
            case PlayerProperty.MoveSpeed:
                GameCoreData.PlayerData.moveSpeed += changeValue;
            break;
        }
        onUpdatePlayerProperty.Invoke(playerProperty,changeValue);
    }

    public void OnChangeBonusCoinCount(int changeValue)
    {
        onChangeBonusCoinCount.Invoke(changeValue);
    }

    public void OnLevelEnd()
    {
        onLevelEnd.Invoke();
    }

    public void OnUpgradeButtonClick()
    {
        onUpgradeButtonClick.Invoke();
    }

    public void OnShopItemPurchase(int cost)
    {
        onShopItemPurchase.Invoke(cost);
    }

    public void OnAddShopItemToInventory(ShopItemData_SO itemData,ShopItem shopItem)
    {
        onAddShopItemToInventory.Invoke(itemData,shopItem);
    }

    public void OnShowShopMenuMask()
    {
        onShowShopMenuMask.Invoke();
    }

    public void OnHideShopMenuMask()
    {
        onHideShopMenuMask.Invoke();
    }

    public void OnCombineWeaponItem(Item_Weapon item_Weapon)
    {
        onCombineWeaponItem.Invoke(item_Weapon);
    }

    public void OnSellWeaponInventoryItems(Item_Weapon item_Weapon)
    {
        onSellWeaponInventoryItems.Invoke(item_Weapon);
    }
}
