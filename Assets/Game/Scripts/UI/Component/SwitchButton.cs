using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private const string path_Img_Background = "Img_Background";
    private const string path_Img_Btn = "Btn_Switch";
    private const string path_LeftPoint = "Img_Background/LeftPoint";
    private const string path_RightPoint = "Img_Background/RightPoint";
    private Image img_Background;
    private Button btn_Switch;
    private Transform trans_LeftPoint;
    private Transform trans_RightPoint;
    [HideInInspector]
    public bool isOn = false;
    public float switchTransitionTime = 0.2f;

    private void Awake() {
        img_Background = transform.Find(path_Img_Background).GetComponent<Image>();
        btn_Switch = transform.Find(path_Img_Btn).GetComponent<Button>();
        trans_LeftPoint = transform.Find(path_LeftPoint);
        trans_RightPoint = transform.Find(path_RightPoint);
    }

    private void OnEnable() {
        btn_Switch.onClick.AddListener(OnBtnSwitchClick);
    }

    private void OnDisable() {
        btn_Switch.onClick.RemoveAllListeners();
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
                btn_Switch.transform.DOMove(trans_RightPoint.position,switchTransitionTime);
                img_Background.DOColor(Color.blue,switchTransitionTime);
            }
            else
            {
                btn_Switch.transform.DOMove(trans_LeftPoint.position,switchTransitionTime);
                img_Background.DOColor(Color.white,switchTransitionTime);
            }
        }
    }

    private void OnBtnSwitchClick()
    {
        if(isOn)
        {
            btn_Switch.transform.DOMove(trans_LeftPoint.position,switchTransitionTime);
            img_Background.DOColor(Color.white,switchTransitionTime);
            isOn = !isOn;
        }
        else
        {
            btn_Switch.transform.DOMove(trans_RightPoint.position,switchTransitionTime);
            img_Background.DOColor(Color.blue,switchTransitionTime);
            isOn = !isOn;
        }
    }
}