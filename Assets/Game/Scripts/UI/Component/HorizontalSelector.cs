using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class HorizontalSelector : MonoBehaviour
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
    [Tooltip("Only select one of these options,if more than one is selected or none of these options is been selected, it'll use the first one in the options")]
    public List<OptionData> selectOptions = new List<OptionData>();

    private void Awake() {
        btn_SelectLeft = transform.Find(path_Btn_SelectLeft).GetComponent<Button>();
        btn_SelectRight = transform.Find(path_Btn_SelectRight).GetComponent<Button>();
        text_Selected = transform.Find(path_Text_Selected).GetComponent<TextMeshProUGUI>();
        InitComponent();
    }

    private void InitComponent()
    {
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
    }
}
