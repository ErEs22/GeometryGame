using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Character : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private const string path_Img_CharacterIcon = "Img_CharacterIcon";
    private Image img_characterIcon;
    public CharacterData_SO CharacterData;
    public bool isSelected = false;

    private void Awake() {
        img_characterIcon = transform.Find(path_Img_CharacterIcon).GetComponent<Image>();
    }

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }

    public void InitUIInfo()
    {
        img_characterIcon.sprite = CharacterData.CharacterIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.instance.OnUpdateSelectCharacterInfo(CharacterData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}