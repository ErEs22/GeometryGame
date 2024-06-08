using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    //------------Path
    protected const string path_Img_ItemIcon = "Img_ItemIcon";
    //------------
    protected Image img_ItemIcon;
    public ShopItemData_SO itemData;

    protected virtual void Awake() {
        img_ItemIcon = transform.Find(path_Img_ItemIcon).GetComponent<Image>();
    }
}