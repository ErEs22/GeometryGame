using System.Collections;
using System.Collections.Generic;
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
    public event UnityAction<int> onInitStatusBar = delegate{};
    public event UnityAction onInitPlayerStatus = delegate{};
    public event UnityAction<Color,Color> onHealthBarFlash = delegate{};
    public event UnityAction<Color,Color> onExpBarFlash = delegate{};
    public event UnityAction onCollectExpBall = delegate{};
    public event UnityAction<int> onShowUpgradeRewardCount = delegate{};
    public event UnityAction onPlayerUpgradeCountIncrease = delegate{};

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
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
}
