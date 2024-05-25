using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatusBar : UIBase
{
    //------------Path
    private string path_Image_Front_Health = "HPBar/FrontBar";
    private string path_Image_Front_Exp = "ExpBar/FrontBar";
    private string path_Text_Health = "HPBar/Text_HPValue";
    private string path_Text_Exp = "ExpBar/Text_ExpValue";
    //------------

    private Image image_Front_Health;
    private Image image_Front_Exp;
    private TextMeshProUGUI text_HealthValue;
    private TextMeshProUGUI text_ExpValue;

    private void Awake() {
        image_Front_Health = transform.Find(path_Image_Front_Health).GetComponent<Image>();
        image_Front_Exp = transform.Find(path_Image_Front_Exp).GetComponent<Image>();
        text_HealthValue = transform.Find(path_Text_Health).GetComponent<TextMeshProUGUI>();
        text_ExpValue = transform.Find(path_Text_Exp).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        EventManager.instance.onUpdateHealthBar += UpdateHealthBar;
        EventManager.instance.onUpdateExpBar += UpdateExpBar;
        EventManager.instance.onInitStatusBar += InitStatusBar;
        EventManager.instance.onHealthBarFlash += HealthBarFlash;
        EventManager.instance.onExpBarFlash += ExpBarFlash;
    }

    private void OnDisable() {
        EventManager.instance.onUpdateHealthBar -= UpdateHealthBar;
        EventManager.instance.onUpdateExpBar -= UpdateExpBar;
        EventManager.instance.onInitStatusBar -= InitStatusBar;
        EventManager.instance.onHealthBarFlash -= HealthBarFlash;
        EventManager.instance.onExpBarFlash -= ExpBarFlash;
    }

    public override void InitUI()
    {
        uiID = UIID.PlayerStatusBar;
    }

    private void InitStatusBar(int maxHP)
    {
        image_Front_Health.fillAmount = 1;
        image_Front_Exp.fillAmount = 0;
        text_HealthValue.text = maxHP + "/" + maxHP;
        text_ExpValue.text = "Lv.1";
    }

    private void UpdateHealthBar(int currentHP,int maxHP)
    {
        text_HealthValue.text = currentHP + "/" + maxHP;
        image_Front_Health.fillAmount = EyreUtility.Divide(currentHP,maxHP);
    }

    private void UpdateExpBar(int currentPlayerLevel,int currentExp,int currentLevelMaxExp)
    {
        text_ExpValue.text = "Lv." + currentPlayerLevel;
        image_Front_Exp.fillAmount = EyreUtility.Divide(currentExp,currentLevelMaxExp);
    }

    private void HealthBarFlash(Color startColor,Color endColor)
    {
        image_Front_Health.DOColor(endColor,0.1f).OnComplete(()=>
        {
            image_Front_Health.DOColor(startColor,0.1f);
        });
    }
    private void ExpBarFlash(Color startColor,Color endColor)
    {
        image_Front_Exp.DOColor(endColor,0.1f).OnComplete(()=>
        {
            image_Front_Exp.DOColor(startColor,0.1f);
        });
    }
}
