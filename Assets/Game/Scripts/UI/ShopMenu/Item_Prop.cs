using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Prop : InventoryItem,IPointerEnterHandler,IPointerExitHandler
{
    protected const string path_Text_ItemCount = "Text_ItemCount";
    protected TextMeshProUGUI text_ItemCount;
    public PropInfoPanel propInfoPanel;
    private int itemPropCount = 0;

    protected override void Awake() {
        base.Awake();
        text_ItemCount = transform.Find(path_Text_ItemCount).GetComponent<TextMeshProUGUI>();
    }

    public void InitItemPropUI(PropInfoPanel propInfoPanel,ShopItemData_SO itemData,int itemLevel)
    {
        this.itemLevel = itemLevel;
        this.itemData = itemData;
        this.propInfoPanel = propInfoPanel;
        img_ItemIcon.sprite = itemData.itemIcon;
        IncreaseItemPropCount();
        SetItemLevelFilterColor();
    }

    public void IncreaseItemPropCount()
    {
        itemPropCount++;
        text_ItemCount.text = "X" + itemPropCount.ToString();
    }

    private void ShowItemInfo()
    {
        propInfoPanel.DisplayPropInfo(itemData as ShopItemData_Prop_SO,itemLevel);
    }

    private void HideItemInfo()
    {
        propInfoPanel.HidePanel();
        propInfoPanel.ClearPropInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowItemInfo();
        Debug.Log("PointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideItemInfo();
        Debug.Log("PointerExit");
    }
}