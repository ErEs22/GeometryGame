using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchButton : Button
{
    private const string path_Img_Background = "Img_SwitchBackground";
    private const string path_Img_Btn = "Btn_Switch";
    private const string path_LeftPoint = "Img_SwitchBackground/LeftPoint";
    private const string path_RightPoint = "Img_SwitchBackground/RightPoint";
    private Image img_Background;
    private Button btn_Switch;
    private Transform trans_LeftPoint;
    private Transform trans_RightPoint;
    [HideInInspector]
    public bool isOn = false;
    public float switchTransitionTime = 0.2f;

    protected override void Awake() {
        base.Awake();
        img_Background = transform.Find(path_Img_Background).GetComponent<Image>();
        btn_Switch = transform.Find(path_Img_Btn).GetComponent<Button>();
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
        if(this.isOn == isOn)
        {
            return;
        }
        else
        {
            this.isOn = isOn;
            if(isOn)
            {
                btn_Switch.transform.position = trans_RightPoint.position;
                img_Background.color = GameColor.switchBtn_On;
            }
            else
            {
                btn_Switch.transform.position = trans_LeftPoint.position;
                img_Background.color = GameColor.switchBtn_Off;
            }
        }
    }

    public void OnBtnSwitchClick()
    {
        if(isOn)
        {
            btn_Switch.transform.DOMove(trans_LeftPoint.position,switchTransitionTime);
            img_Background.DOColor(GameColor.switchBtn_Off,switchTransitionTime);
            isOn = !isOn;
        }
        else
        {
            btn_Switch.transform.DOMove(trans_RightPoint.position,switchTransitionTime);
            img_Background.DOColor(GameColor.switchBtn_On,switchTransitionTime);
            isOn = !isOn;
        }
    }
}