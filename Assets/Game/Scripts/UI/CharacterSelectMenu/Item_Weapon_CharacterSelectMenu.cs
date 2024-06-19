using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Weapon_CharacterSelectMenu : Item_Weapon
{
    private const string path_Img_ItemSelectMark = "Img_ItemSelectMark";
    private Image img_ItemSelectMark;
    public UICharacterSelectMenu uICharacterSelectMenu;
    public bool isSelected = false;

    protected override void Awake()
    {
        base.Awake();
        img_ItemSelectMark = transform.Find(path_Img_ItemSelectMark).GetComponent<Image>();
        img_ItemSelectMark.gameObject.SetActive(false);
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
        img_ItemSelectMark.gameObject.SetActive(true);
        uICharacterSelectMenu.SelectWeapon(this);
    }

    public void UnSelectWeapon()
    {
        isSelected = false;
        img_ItemSelectMark.gameObject.SetActive(false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        uICharacterSelectMenu.UpdateSelectingWeaponInfo(itemData as ShopItemData_Weapon_SO);
        img_ItemSelectMark.gameObject.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(!isSelected)
        {
            img_ItemSelectMark.gameObject.SetActive(false);
        }
        uICharacterSelectMenu.UpdateSelectedWeaponInfo();
    }
}