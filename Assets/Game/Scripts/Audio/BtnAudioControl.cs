using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class BtnAudioControl : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{
    public bool clickSFX = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hoverAudioSFX);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickSFX)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.clickAudioSFX);
        }
    }

}