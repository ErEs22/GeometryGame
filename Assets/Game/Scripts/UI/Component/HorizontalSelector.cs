using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class HorizontalSelector : MonoBehaviour,IMoveHandler
{
    [Serializable]
    public class OptionData
    {
        public string optionName;
        public bool defaultOption = false;
    }
    private const string path_Btn_SelectLeft = "Btn_SelectLeft";
    private const string path_Btn_SelectRight = "Btn_SelectRight";
    private const string path_Text_Selected = "Text_Selected";
    private Button btn_SelectLeft;
    private Button btn_SelectRight;
    private TextMeshProUGUI text_Selected;
    public int selectIndex = 0;
    [Tooltip("Only select one of these options,if more than one is selected or none of these options is been selected, it'll use the first one in the options")]
    public List<OptionData> selectOptions = new List<OptionData>();

    private void Awake() {
        btn_SelectLeft = transform.Find(path_Btn_SelectLeft).GetComponent<Button>();
        btn_SelectRight = transform.Find(path_Btn_SelectRight).GetComponent<Button>();
        text_Selected = transform.Find(path_Text_Selected).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        btn_SelectLeft.onClick.AddListener(OnBtnSelectLeftClick);
        btn_SelectRight.onClick.AddListener(OnBtnSelectRightClick);
    }

    private void OnDisable() {
        btn_SelectLeft.onClick.RemoveAllListeners();
        btn_SelectRight.onClick.RemoveAllListeners();
    }

    private void OnBtnSelectLeftClick()
    {
        if(selectIndex > 0)
        {
            selectIndex--;
            text_Selected.text = selectOptions[selectIndex].optionName;
        }
    }

    private void OnBtnSelectRightClick()
    {
        if(selectIndex < selectOptions.Count - 1)
        {
            selectIndex++;
            text_Selected.text = selectOptions[selectIndex].optionName;
        }
    }

    public void InitComponent(string optionName)
    {
        foreach (OptionData option in selectOptions)
        {
            if(option.optionName == optionName)
            {
                text_Selected.text = optionName;
                selectIndex = selectOptions.IndexOf(option);
                return;
            }
        }
        /*
        int selectedCount = 0;
        string optionName = "";
        foreach (OptionData option in selectOptions)
        {
            if(option.defaultOption)
            {
                selectedCount++;
                optionName = option.optionName;
            }
        }
        if(selectedCount <= 0 || selectedCount > 1)
        {
            text_Selected.text = selectOptions[0].optionName;
        }
        else
        {
            text_Selected.text = optionName;
        }
        */
    }

    public void OnMove(AxisEventData eventData)
    {
        switch(eventData.moveDir)
        {
            case MoveDirection.Left:
                if(selectIndex > 0)
                {
                    selectIndex--;
                    text_Selected.text = selectOptions[selectIndex].optionName;
                }
            break;
            case MoveDirection.Right:
                if(selectIndex < selectOptions.Count - 1)
                {
                    selectIndex++;
                    text_Selected.text = selectOptions[selectIndex].optionName;
                }
            break;
            case MoveDirection.Up:
            break;
            case MoveDirection.Down:
            break;
            case MoveDirection.None:
            break;
            default:break;
        }
    }
}
