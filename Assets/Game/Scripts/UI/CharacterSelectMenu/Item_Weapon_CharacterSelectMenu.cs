using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Weapon_CharacterSelectMenu : Item_Weapon
{
    public bool isSelected = false;
    public override void InitItemPropUI(WeaponInfoPanel weaponInfoPanel, ShopItemData_SO itemData, int itemLevel)
    {
        this.itemData = itemData;
        img_ItemIcon.sprite = itemData.itemIcon;
    }

    protected override void OnBtnItemClick()
    {
        isSelected = true;
        EventManager.instance.OnSelectFirstWeapon(this);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.instance.OnUpdateSelectWeaponInfo(itemData as ShopItemData_Weapon_SO);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        
    }
}