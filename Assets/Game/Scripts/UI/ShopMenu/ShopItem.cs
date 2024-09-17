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
    protected const string path_Text_ItemRareLevel = "ItemInfo/Text_ItemRareLevel";
    protected const string path_PropertiesParent = "ItemInfo/Properties";
    protected const string path_Btn_Purchase = "Btn_Purchase";
    protected const string path_Btn_Lock = "Btn_Lock";
    protected const string path_Img_ItemLevelFilter = "Img_ItemLevelFilter";
    //------------

    protected Image img_ItemIcon;
    protected TextMeshProUGUI text_ItemName;
    protected TextMeshProUGUI text_ItemRareLevel;
    protected TextMeshProUGUI text_Btn_Purchase;
    protected Button btn_Purchase;
    protected Button btn_Lock;
    protected Transform trans_PropertiesParent;
    protected Image img_ItemLevelFilter;
    public bool isLocked = false;
    public bool isAffordable = true;
    public bool isPruchased = false;
    public int itemLevel = 1;
    public int price = 0;

    protected void Awake()
    {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemRareLevel = transform.Find(path_Text_ItemRareLevel).GetComponent<TextMeshProUGUI>();
        btn_Purchase = transform.Find(path_Btn_Purchase).GetComponent<Button>();
        text_Btn_Purchase = btn_Purchase.GetComponentInChildren<TextMeshProUGUI>();
        btn_Lock = transform.Find(path_Btn_Lock).GetComponent<Button>();
        img_ItemLevelFilter = transform.Find(path_Img_ItemLevelFilter).GetComponent<Image>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);

    }

    public virtual void UpdateUIInfo()
    {

    }

    public void SetItemInvisible()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    protected int CaculateItemPrice(int itemBaseCost,int itemBaseLevel,int itemCurrentLevel)
    {
        price = itemBaseCost / itemBaseLevel * itemCurrentLevel;
        return price;
    }

    protected void SetItemRareLevelText(int itemLevel)
    {
        switch (itemLevel)
        {
            case 1:
                text_ItemRareLevel.text = "普通";
                text_ItemRareLevel.color = GameColor.ShopItem_Level01;
                break;
            case 2:
                text_ItemRareLevel.text = "特殊";
                text_ItemRareLevel.color = GameColor.ShopItem_Level02;
                break;
            case 3:
                text_ItemRareLevel.text = "稀有";
                text_ItemRareLevel.color = GameColor.ShopItem_Level03;
                break;
            case 4:
                text_ItemRareLevel.text = "传说";
                text_ItemRareLevel.color = GameColor.ShopItem_Level04;
                break;
            case 5:
                text_ItemRareLevel.color = GameColor.ShopItem_Level05;
                break;
            default: break;
        }
    }
}
