using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateFPS();
    }

    private async void UpdateFPS()
    {
        while(true)
        {
            text.text = "FPS:" + Mathf.Floor(1.0f / Time.deltaTime).ToString();
            await UniTask.Delay(1000);
        }
    }
}
