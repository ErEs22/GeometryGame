using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    //------------Path
    protected const string path_Img_ItemIcon = "ItemInfo/Img_ItemIcon";
    protected const string path_Text_ItemName = "ItemInfo/Text_ItemName";
    protected const string path_PropertiesParent = "ItemInfo/Properties";
    protected const string path_Btn_Purchase = "Btn_Purchase";
    protected const string path_Btn_Lock = "Btn_Lock";
    protected const string path_Img_HideMask = "HideMask";
    protected const string path_Img_ItemLevelFilter = "Img_ItemLevelFilter";
    //------------
    
    protected Image img_ItemIcon;
    protected TextMeshProUGUI text_ItemName;
    protected Button btn_Purchase;
    protected Button btn_Lock;
    public Image img_HideMask;
    protected Transform trans_PropertiesParent;
    protected Image img_ItemLevelFilter;
    public bool isLocked = false;
    public bool isAffordable = true;
    public int itemLevel = 1;

    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        btn_Purchase = transform.Find(path_Btn_Purchase).GetComponent<Button>();
        btn_Lock = transform.Find(path_Btn_Lock).GetComponent<Button>();
        img_HideMask = transform.Find(path_Img_HideMask).GetComponent<Image>();
        img_ItemLevelFilter = transform.Find(path_Img_ItemLevelFilter).GetComponent<Image>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);

        img_HideMask.gameObject.SetActive(false);
    }

    public virtual void UpdateUIInfo()
    {

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
