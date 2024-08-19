using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class BtnAudioControl : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{

    public AudioData hoverAudioData;
    public AudioData clickAudioData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(hoverAudioData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(clickAudioData);
    }

}