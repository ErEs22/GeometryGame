using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Character : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private const string path_Img_CharacterIcon = "Img_CharacterIcon";
    private const string path_Img_Background = "Img_Background";
    private Image img_Background;
    private Image img_CharacterIcon;
    private Button btn_Self;
    public UICharacterSelectMenu uICharacterSelectMenu;
    public CharacterData_SO characterData;
    public bool isSelected = false;

    private void Awake() {
        btn_Self = GetComponent<Button>();
        img_Background = transform.Find(path_Img_Background).GetComponent<Image>();
        img_CharacterIcon = transform.Find(path_Img_CharacterIcon).GetComponent<Image>();
        img_Background.color = GameColor.btn_Normal;
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
        img_Background.color = GameColor.btn_Select;
    }

    public void UnSelectCharacter()
    {
        isSelected = false;
        img_Background.color = GameColor.btn_Normal;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uICharacterSelectMenu.UpdateSelectingCharacterInfo(characterData);
        img_Background.color = GameColor.btn_Select;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isSelected)
        {
            img_Background.color = GameColor.btn_Normal;
        }
        uICharacterSelectMenu.UpdateSelectedCharacterInfo();
    }
}