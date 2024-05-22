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
}
