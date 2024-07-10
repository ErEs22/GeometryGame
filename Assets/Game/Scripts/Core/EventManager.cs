using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event UnityAction<int,int,int> onUICountDown = delegate{};
    public event UnityAction<eUIID> onOpenUI = delegate{};
    public event UnityAction<eUIID> onCloseUI = delegate{};
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
    public event UnityAction<ePlayerProperty,int> onUpdatePlayerProperty = delegate{};
    public event UnityAction<int> onChangeBonusCoinCount = delegate{};
    public event UnityAction onLevelEnd = delegate{};
    public event UnityAction onUpgradeButtonClick = delegate{};
    public event UnityAction<int> onShopItemPurchase = delegate{};
    public event UnityAction<ShopItemData_SO,ShopItem> onAddShopItemToInventory = delegate{};
    public event UnityAction onShowShopMenuMask = delegate{};
    public event UnityAction onHideShopMenuMask = delegate{};
    public event UnityAction<Item_Weapon> onCombineWeaponItem = delegate{};
    public event UnityAction<Item_Weapon> onSellWeaponInventoryItems = delegate{};
    public event UnityAction onStartGame = delegate{};
    public event UnityAction onGenerateWeaonInInventory = delegate{};
    public event UnityAction onDisableLocomotionInput = delegate{};
    public event UnityAction onEnableLocomotionInput = delegate {};
    public event UnityAction<int,GameObject,bool> onDamageDisplay = delegate{};

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OnDamageDisplay(int damage,GameObject damageObject,bool isCritical)
    {
        onDamageDisplay.Invoke(damage, damageObject, isCritical);
    }

    public void OnEnableLocomotionInput()
    {
        onEnableLocomotionInput.Invoke();
    }

    public void OnDisableLocomotionInput()
    {
        onDisableLocomotionInput.Invoke();
    }

    public void OnGenerateWeaonInInventory()
    {
        onGenerateWeaonInInventory.Invoke();
    }

    public void OnStartGame()
    {
        onStartGame.Invoke();
    }

    public void OnUICountDown(int start,int end,int interval)
    {
        onUICountDown.Invoke(start,end,interval);
    }

    public void OnOpenUI(eUIID id)
    {
        onOpenUI.Invoke(id);
    }

    public void OnCloseUI(eUIID id)
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

    public void OnUpdatePlayerProperty(ePlayerProperty playerProperty,int changeValue)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP://TODO血量修改超过最大\小值时需被限制在范围内
                GameCoreData.PlayerProperties.maxHP += changeValue;
            break;
            case ePlayerProperty.HPRegeneration:
                GameCoreData.PlayerProperties.hpRegeneration += changeValue;
            break;
            case ePlayerProperty.lifeSteal:
                GameCoreData.PlayerProperties.lifeSteal += changeValue;
            break;
            case ePlayerProperty.DamageMul:
                GameCoreData.PlayerProperties.damageMul += changeValue;
            break;
            case ePlayerProperty.AttackSpeed:
                GameCoreData.PlayerProperties.attackSpeedMul += changeValue;
            break;
            case ePlayerProperty.CriticalRate:
                GameCoreData.PlayerProperties.criticalRate += changeValue;
            break;
            case ePlayerProperty.AttackRange:
                GameCoreData.PlayerProperties.attackRange += changeValue;
            break;
            case ePlayerProperty.MoveSpeed:
                GameCoreData.PlayerProperties.moveSpeed += changeValue;
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
