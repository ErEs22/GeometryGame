using UnityEngine;

public class UIPauseBtn : MonoBehaviour
{
    private void Awake() {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPauseBtnClick()
    {
        EventManager.instance.OnOpenUI(eUIID.PauseMenu);
        Time.timeScale = 0;
    }
}