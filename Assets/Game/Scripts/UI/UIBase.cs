using UnityEngine;

public class UIBase :MonoBehaviour
{
    [DisplayOnly]
    public UIID uiID;

    private void Awake() {
        InitUI();
    }

    protected virtual void InitUI()
    {

    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}