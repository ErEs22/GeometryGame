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
    //------------
    
    protected Image img_ItemIcon;
    protected TextMeshProUGUI text_ItemName;
    protected Button btn_Purchase;
    protected Button btn_Lock;
    public Image img_HideMask;
    protected Transform trans_PropertiesParent;
    public bool isLocked = false;
    public bool isAffordable = true;

    protected void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        btn_Purchase = transform.Find(path_Btn_Purchase).GetComponent<Button>();
        btn_Lock = transform.Find(path_Btn_Lock).GetComponent<Button>();
        img_HideMask = transform.Find(path_Img_HideMask).GetComponent<Image>();
        trans_PropertiesParent = transform.Find(path_PropertiesParent);

        img_HideMask.gameObject.SetActive(false);
    }

    public virtual void UpdateUIInfo()
    {

    }
}
