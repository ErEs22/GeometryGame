using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchButton : Button
{
    private const string path_Img_Background = "Img_SwitchBackground";
    private const string path_Img_Btn = "Btn_Switch";
    private const string path_LeftPoint = "Img_SwitchBackground/LeftPoint";
    private const string path_RightPoint = "Img_SwitchBackground/RightPoint";
    private const string path_Text_SwitchOFF = "Text_Switch/Text_OFF";
    private const string path_Text_SwitchON = "Text_Switch/Text_ON";
    private Image img_Background;
    private Button btn_Switch;
    private TextMeshProUGUI text_SwitchOFF;
    private TextMeshProUGUI text_SwitchON;
    private Transform trans_LeftPoint;
    private Transform trans_RightPoint;
    [HideInInspector]
    public bool isOn = false;
    public float switchTransitionTime = 0.2f;

    protected override void Awake() {
        base.Awake();
        img_Background = transform.Find(path_Img_Background).GetComponent<Image>();
        btn_Switch = transform.Find(path_Img_Btn).GetComponent<Button>();
        text_SwitchOFF = transform.Find(path_Text_SwitchOFF).GetComponent<TextMeshProUGUI>();
        text_SwitchON = transform.Find(path_Text_SwitchON).GetComponent<TextMeshProUGUI>();
        trans_LeftPoint = transform.Find(path_LeftPoint);
        trans_RightPoint = transform.Find(path_RightPoint);
    }

    protected override void OnEnable() {
        base.OnEnable();
        btn_Switch.onClick.AddListener(OnBtnSwitchClick);
    }

    protected override void OnDisable() {
        base.OnDisable();
        btn_Switch.onClick.RemoveAllListeners();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        OnBtnSwitchClick();
    }

    public void SetButtonStatus(bool isOn)
    {
        this.isOn = isOn;
        if(isOn)
        {
            btn_Switch.transform.position = trans_RightPoint.position;
            text_SwitchON.color = GameColor.switchBtn_On;
            text_SwitchOFF.color = GameColor.switchBtn_Off;
            // img_Background.color = GameColor.switchBtn_On;
        }
        else
        {
            btn_Switch.transform.position = trans_LeftPoint.position;
            text_SwitchOFF.color = GameColor.switchBtn_On;
            text_SwitchON.color = GameColor.switchBtn_Off;
            // img_Background.color = GameColor.switchBtn_Off;
        }
        // if(this.isOn == isOn)
        // {
        //     return;
        // }
        // else
        // {
        // }
    }

    public void OnBtnSwitchClick()
    {
        if(isOn)
        {
            btn_Switch.transform.DOMove(trans_LeftPoint.position,switchTransitionTime);
                text_SwitchOFF.color = GameColor.switchBtn_On;
                text_SwitchON.color = GameColor.switchBtn_Off;
            // img_Background.DOColor(GameColor.switchBtn_Off,switchTransitionTime);
            isOn = !isOn;
        }
        else
        {
            btn_Switch.transform.DOMove(trans_RightPoint.position,switchTransitionTime);
                text_SwitchON.color = GameColor.switchBtn_On;
                text_SwitchOFF.color = GameColor.switchBtn_Off;
            // img_Background.DOColor(GameColor.switchBtn_On,switchTransitionTime);
            isOn = !isOn;
        }
    }
}