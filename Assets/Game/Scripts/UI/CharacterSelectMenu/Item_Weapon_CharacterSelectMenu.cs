using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Weapon_CharacterSelectMenu : Item_Weapon
{
    private const string path_Img_Background = "Img_Background";
    private Image img_Background;
    public UICharacterSelectMenu uICharacterSelectMenu;
    public bool isSelected = false;

    protected override void Awake()
    {
        base.Awake();
        img_Background = transform.Find(path_Img_Background).GetComponent<Image>();
        img_Background.color = GameColor.btn_Normal;
    }

    public void InitItemPropUI(ShopItemData_SO itemData,UICharacterSelectMenu uICharacterSelectMenu)
    {
        this.uICharacterSelectMenu = uICharacterSelectMenu;
        this.itemData = itemData;
        img_ItemIcon.sprite = itemData.itemIcon;
    }

    protected override void OnBtnItemClick()
    {
        isSelected = true;
        img_Background.color = GameColor.btn_Select;
        uICharacterSelectMenu.SelectWeapon(this);
    }

    public void UnSelectWeapon()
    {
        isSelected = false;
        img_Background.color = GameColor.btn_Normal;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        uICharacterSelectMenu.UpdateSelectingWeaponInfo(itemData as ShopItemData_Weapon_SO);
        img_Background.color = GameColor.btn_Select;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(!isSelected)
        {
            img_Background.color = GameColor.btn_Normal;
        }
        uICharacterSelectMenu.UpdateSelectedWeaponInfo();
    }
}