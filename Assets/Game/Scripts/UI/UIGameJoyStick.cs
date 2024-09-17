using UnityEngine;

public class UIGameJoyStick : MonoBehaviour
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
}