using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Weapon : InventoryItem,IPointerEnterHandler,IPointerExitHandler
{
    public WeaponInfoPanel weaponInfoPanel;
    public Inventory_Weapon inventory_Weapon;
    private Button btn_Item;
    private bool isClicked = false;
    private int sellPrice = 0;

    protected override void Awake()
    {
        base.Awake();
        btn_Item = GetComponent<Button>();
    }

    private void OnEnable() {
        btn_Item.onClick.AddListener(OnBtnItemClick);
    }

    private void OnDisable() {
        btn_Item.onClick.RemoveAllListeners();
    }

    public void Combine()
    {
        Destroy(gameObject);
    }

    public void UpgradeItemLevel()
    {
        itemLevel++;
        SetItemLevelFilterColor();
    }

    protected virtual void OnBtnItemClick()
    {
        isClicked = true;
        EventManager.instance.OnShowShopMenuMask();
        weaponInfoPanel.transform.position = transform.position + new Vector3(120,340);
        weaponInfoPanel.activeItem = this;
    }

    public virtual void InitItemPropUI(WeaponInfoPanel weaponInfoPanel,ShopItemData_SO itemData,int itemLevel,int sellPrice)
    {
        this.itemLevel = itemLevel;
        this.itemData = itemData;
        this.sellPrice = sellPrice;
        img_ItemIcon.sprite = itemData.itemIcon;
        this.weaponInfoPanel = weaponInfoPanel;
        SetItemLevelFilterColor();
    }

    private void ShowItemInfo()
    {
        weaponInfoPanel.DisplayPropInfo(itemData as ShopItemData_Weapon_SO,itemLevel);
    }

    private void HideItemInfo()
    {
        weaponInfoPanel.HidePanel();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if(isClicked) return;
        HideItemInfo();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isClicked = false;
        weaponInfoPanel.activeItem = this;
        ShowItemInfo();
    }
}