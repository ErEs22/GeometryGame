using TMPro;
using UnityEngine;

public class LevelTextDisplay : MonoBehaviour
{
    TextMeshProUGUI text_Level;

    private void Awake() {
        text_Level = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        EventManager.instance.onLevelTextUpdate += UpdateTextContent;
    }

    private void OnDisable() {
        EventManager.instance.onLevelTextUpdate -= UpdateTextContent;
    }

    private void UpdateTextContent(int currentLevel)
    {
        text_Level.text = "关卡:" + currentLevel.ToString();
    }
}