using UnityEngine;

public class UIBase :MonoBehaviour
{
    [DisplayOnly]
    public UIID uiID;

    private void Awake() {
        // InitUI();
    }

    public virtual void InitUI()
    {

    }

    public virtual void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}