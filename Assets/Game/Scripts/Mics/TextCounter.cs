using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextCounter : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    private void Awake() {
        textComp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        EventManager.instance.onUICountDown += TextCountDown;
    }

    private void OnDisable() {
        EventManager.instance.onUICountDown -= TextCountDown;
    }

    private async void TextCountDown(int start,int end,int interval)
    {
        int maxValue = start > end ? start : end;
        int minValue = start > end ? end : start;
        int intervalSymbol = (end - start) / Mathf.Abs(end - start) * interval;
        int countTimes = EyreUtility.Round(Mathf.Ceil(EyreUtility.Divide(Mathf.Abs(end - start),interval)));
        textComp.text = "剩余时间:" + start.ToString();

        for(int i = 0; i < countTimes; i++)
        {
            start += intervalSymbol;
            Mathf.Clamp(start,minValue,maxValue);
            textComp.text = "剩余时间:" + start.ToString();
            await UniTask.Delay(interval * 1000);
            if(GlobalVar.gameStatus == eGameStatus.Ended || GlobalVar.gameStatus == eGameStatus.MainMenu)
            {
                ClearTextCountDown();
                return;
            }
        }
    }

    private void ClearTextCountDown()
    {
        textComp.text = "0";
    }
}
