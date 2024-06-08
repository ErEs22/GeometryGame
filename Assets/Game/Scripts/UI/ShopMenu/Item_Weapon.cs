using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Weapon : InventoryItem,IPointerEnterHandler,IPointerExitHandler
{
    public WeaponInfoPanel weaponInfoPanel;

    public void InitItemPropUI(WeaponInfoPanel weaponInfoPanel,ShopItemData_SO itemData)
    {
        this.itemData = itemData;
        this.weaponInfoPanel = weaponInfoPanel;
        img_ItemIcon.sprite = itemData.itemIcon;
    }

    private void ShowItemInfo()
    {
        weaponInfoPanel.transform.position = transform.position + new Vector3(120,210);
        weaponInfoPanel.DisplayPropInfo(itemData as ShopItemData_Weapon_SO);
    }

    private void HideItemInfo()
    {
        weaponInfoPanel.transform.position = new Vector3(-9999,-9999);
        weaponInfoPanel.ClearPropInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideItemInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowItemInfo();
    }
}