using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISettingOption_SwitchButton : UISettingOption,ISubmitHandler
{
    private SwitchButton switchButton;

    private void Awake() {
        switchButton = GetComponent<SwitchButton>();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        switchButton.OnBtnSwitchClick();
    }


}