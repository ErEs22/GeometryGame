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
    protected const string path_Properties = "ItemInfo/Properties";
    protected const string path_Btn_Purchase = "Btn_Purchase";
    protected const string path_Btn_Lock = "Btn_Lock";
    //------------
    
    protected Image Img_ItemIcon;
    protected TextMeshProUGUI text_ItemName;
    protected Button btn_Purchase;
    protected Button btn_Lock;
    protected Transform propertiesTrans;
    public bool isLocked = false;
    public bool isAffordable = true;

    protected void Awake() {
        Img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        btn_Purchase = transform.Find(path_Btn_Purchase).GetComponent<Button>();
        btn_Lock = transform.Find(path_Btn_Lock).GetComponent<Button>();
        propertiesTrans = transform.Find(path_Properties);
    }

    public virtual void UpdateUIInfo()
    {

    }
}
