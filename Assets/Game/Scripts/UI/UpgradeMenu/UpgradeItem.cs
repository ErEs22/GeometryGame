using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    //-----------Path
    private string path_Image_Icon = "Icon";
    private string path_Text_ItemName = "Text_ItemName";
    private string path_Text_ItemDescription = "Text_ItemDesc";
    private string path_Btn_Upgrade = "Btn_Upgrade";
    //-----------
    private Image icon;
    private TextMeshProUGUI text_ItemName;
    private TextMeshProUGUI text_ItemDescription;
    private Button btn_Upgrade;
    private int itemLevel;
    private UpgradeItemData_SO itemData;

    private void Awake() {
        BindFields();
    }

    private void OnEnable() {
        btn_Upgrade.onClick.AddListener(OnUpgradeBtnClick);
    }

    private void OnDisable() {
        btn_Upgrade.onClick.RemoveAllListeners();
    }

    private void BindFields()
    {
        icon = transform.Find(path_Image_Icon).GetComponent<Image>();
        text_ItemName = transform.Find(path_Text_ItemName).GetComponent<TextMeshProUGUI>();
        text_ItemDescription = transform.Find(path_Text_ItemDescription).GetComponent<TextMeshProUGUI>();
        btn_Upgrade = transform.Find(path_Btn_Upgrade).GetComponent<Button>();
    }

    public void Init(UpgradeItemData_SO itemData,int itemLevel)
    {
        this.itemData = itemData;
        icon.sprite = itemData.icon;
        text_ItemName.text = itemData.itemName + " " + itemLevel;
        this.itemLevel = itemLevel;
        string sign = itemData.valueAmount > 0 ? "+" : "-";
        switch(itemData.effectProperty)
        {
            case PlayerProperty.MaxHP:
                text_ItemDescription.text = sign + itemData.valueAmount + "HP";
            break;
            case PlayerProperty.HPRegeneration:
                text_ItemDescription.text = sign + itemData.valueAmount + "HPRegeneration";
            break;
            case PlayerProperty.StealHP:
                text_ItemDescription.text = sign + itemData.valueAmount + "%StealHP";
            break;
            case PlayerProperty.DamageMul:
                text_ItemDescription.text = sign + itemData.valueAmount + "%Damage";
            break;
            case PlayerProperty.AttackSpeed:
                text_ItemDescription.text = sign + itemData.valueAmount + "%AttackSpeed";
            break;
            case PlayerProperty.CriticalRate:
                text_ItemDescription.text = sign + itemData.valueAmount + "%CriticalRate";
            break;
            case PlayerProperty.AttackRange:
                text_ItemDescription.text = sign + itemData.valueAmount + "AttackRange";
            break;
            case PlayerProperty.MoveSpeed:
                text_ItemDescription.text = sign + itemData.valueAmount + "%MoveSpeed";
            break;
        }
    }

    private void OnUpgradeBtnClick()
    {
        EventManager.instance.OnUpdatePlayerProperty(itemData.effectProperty,itemData.valueAmount);
        EventManager.instance.OnUpgradeButtonClick();
    }
}
