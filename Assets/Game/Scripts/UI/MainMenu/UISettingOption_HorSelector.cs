using UnityEngine;
using UnityEngine.EventSystems;

public class UISettingOption_HorSelector : UISettingOption,IMoveHandler
{
    public void OnMove(AxisEventData eventData)
    {
        HorizontalSelector horSelectorComp = selectableComp.GetComponent<HorizontalSelector>();
        horSelectorComp.OnMove(eventData);
    }
}