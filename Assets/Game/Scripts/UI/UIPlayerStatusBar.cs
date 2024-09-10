using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatusBar : UIBase
{
    //------------Path
    private string path_Image_Front_Health = "Status/HPBar/Img_Background/FrontBar";
    private string path_Image_Front_Exp = "Status/ExpBar/Img_Background/FrontBar";
    private string path_Image_PlayerIcon = "Status/PlayerIcon/Img_PlayerIcon";
    private string path_Text_Exp = "Status/Text_ExpValue";
    private string path_Text_CoinCount = "Coin/Text_CoinCount";
    private string path_Text_BonusCoinCount = "BonusCoin/Text_BonusCoinCount";
    //------------

    private Image image_Front_Health;
    private Image image_Front_Exp;
    private Image image_playerIcon;
    private TextMeshProUGUI text_ExpValue;
    private TextMeshProUGUI text_CoinCount;
    private TextMeshProUGUI text_BonusCoinCount;

    private void Awake() {
        image_Front_Health = transform.Find(path_Image_Front_Health).GetComponent<Image>();
        image_Front_Exp = transform.Find(path_Image_Front_Exp).GetComponent<Image>();
        image_playerIcon = transform.Find(path_Image_PlayerIcon).GetComponent<Image>();
        text_ExpValue = transform.Find(path_Text_Exp).GetComponent<TextMeshProUGUI>();
        text_CoinCount = transform.Find(path_Text_CoinCount).GetComponent<TextMeshProUGUI>();
        text_BonusCoinCount = transform.Find(path_Text_BonusCoinCount).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        EventManager.instance.onUpdateHealthBar += UpdateHealthBar;
        EventManager.instance.onUpdateExpBar += UpdateExpBar;
        EventManager.instance.onInitStatusBar += InitStatusBar;
        EventManager.instance.onHealthBarFlash += HealthBarFlash;
        EventManager.instance.onExpBarFlash += ExpBarFlash;
        EventManager.instance.onUpdateCoinCount += UpdateCoinCount;
        EventManager.instance.onUpdateBonusCoinCount += UpdateBonusCoinCount;
        EventManager.instance.onSetCharacterData += SetCharacterIcon;
    }

    private void OnDisable() {
        EventManager.instance.onUpdateHealthBar -= UpdateHealthBar;
        EventManager.instance.onUpdateExpBar -= UpdateExpBar;
        EventManager.instance.onInitStatusBar -= InitStatusBar;
        EventManager.instance.onHealthBarFlash -= HealthBarFlash;
        EventManager.instance.onExpBarFlash -= ExpBarFlash;
        EventManager.instance.onUpdateCoinCount -= UpdateCoinCount;
        EventManager.instance.onUpdateBonusCoinCount -= UpdateBonusCoinCount;
        EventManager.instance.onSetCharacterData -= SetCharacterIcon;
    }

    public override void InitUI()
    {
        uiID = eUIID.PlayerStatusBar;
    }

    private void SetCharacterIcon(CharacterData_SO data)
    {
        image_playerIcon.sprite = data.characterIcon;
    }

    private void InitStatusBar(int maxHP)
    {
        image_Front_Health.fillAmount = 1;
        image_Front_Exp.fillAmount = 0;
        text_ExpValue.text = "LV.1";
        text_CoinCount.text = "0";
        text_BonusCoinCount.text = "0";
    }

    private void UpdateHealthBar(int currentHP,int maxHP)
    {
        image_Front_Health.fillAmount = EyreUtility.Divide(currentHP,maxHP);
    }

    private void UpdateExpBar(int currentPlayerLevel,int currentExp,int currentLevelMaxExp)
    {
        text_ExpValue.text = "LV." + currentPlayerLevel;
        image_Front_Exp.fillAmount = EyreUtility.Divide(currentExp,currentLevelMaxExp);
    }

    private void UpdateCoinCount()
    {
        text_CoinCount.text = GameCoreData.PlayerProperties.coin.ToString();
    }

    private void UpdateBonusCoinCount(int bonusCoinCount)
    {
        text_BonusCoinCount.text = bonusCoinCount.ToString();
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
