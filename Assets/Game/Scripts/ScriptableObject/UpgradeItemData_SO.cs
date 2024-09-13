using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Upgrade",fileName = "UpgradeItemData")]
public class UpgradeItemData_SO : ScriptableObject
{
    //数值计算模式为百分比模式
    public Sprite icon;
    public string itemName;
    public int valueAmount;
    public ePlayerProperty effectProperty;
}