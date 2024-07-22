using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Character : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private const string path_Img_CharacterIcon = "Img_CharacterIcon";
    private const string path_Img_SelectMark = "Img_ItemSelectMark";
    private Image img_CharacterIcon;
    private Image img_ItemSelectMark;
    private Button btn_Self;
    public UICharacterSelectMenu uICharacterSelectMenu;
    public CharacterData_SO characterData;
    public bool isSelected = false;

    private void Awake() {
        btn_Self = GetComponent<Button>();
        img_CharacterIcon = transform.Find(path_Img_CharacterIcon).GetComponent<Image>();
        img_ItemSelectMark = transform.Find(path_Img_SelectMark).GetComponent<Image>();
        img_ItemSelectMark.gameObject.SetActive(false);
    }

    private void OnEnable() {
        btn_Self.onClick.AddListener(OnBtnSelfClick);
    }

    private void OnDisable() {
        btn_Self.onClick.RemoveAllListeners();
    }

    public void InitUIInfo(CharacterData_SO characterData_SO,UICharacterSelectMenu uICharacterSelectMenu)
    {
        characterData = characterData_SO;
        this.uICharacterSelectMenu = uICharacterSelectMenu;
        img_CharacterIcon.sprite = characterData.characterIcon;
    }

    private void OnBtnSelfClick()
    {
        isSelected = true;
        uICharacterSelectMenu.SelectCharacter(this);
        img_ItemSelectMark.gameObject.SetActive(true);
    }

    public void UnSelectCharacter()
    {
        isSelected = false;
        img_ItemSelectMark.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uICharacterSelectMenu.UpdateSelectingCharacterInfo(characterData);
        img_ItemSelectMark.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isSelected)
        {
            img_ItemSelectMark.gameObject.SetActive(false);
        }
        uICharacterSelectMenu.UpdateSelectedCharacterInfo();
    }
}