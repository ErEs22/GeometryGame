using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISettingOption : MonoBehaviour,ISelectHandler
{
    [SerializeField]
    protected Selectable selectableComp;

    public virtual void OnSelect(BaseEventData eventData)
    {
        selectableComp.Select();
    }
}