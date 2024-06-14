using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    //------------Path
    protected const string path_Img_ItemIcon = "Img_ItemIcon";
    protected const string path_Img_ItemLevelFilter = "Img_ItemLevelFilter";
    //------------
    protected Image img_ItemIcon;
    protected Image img_ItemLevelFilter;
    [DisplayOnly]
    public int itemLevel = 1;
    public ShopItemData_SO itemData;

    protected virtual void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        img_ItemLevelFilter = transform.Find(path_Img_ItemLevelFilter).GetComponent<Image>();
    }

    protected void SetItemLevelFilterColor()
    {
        switch(itemLevel)
        {
            case 1:
                img_ItemLevelFilter.color = GameColor.ShopItem_Level01;
            break;
            case 2:
                img_ItemLevelFilter.color = GameColor.ShopItem_Level02;
            break;
            case 3:
                img_ItemLevelFilter.color = GameColor.ShopItem_Level03;
            break;
            case 4:
                img_ItemLevelFilter.color = GameColor.ShopItem_Level04;
            break;
            case 5:
                img_ItemLevelFilter.color = GameColor.ShopItem_Level05;
            break;
            default:break;
        }
    }
}