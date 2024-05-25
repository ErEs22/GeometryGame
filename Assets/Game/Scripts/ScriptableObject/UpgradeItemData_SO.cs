using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Upgrade",fileName = "UpgradeItemData")]
public class UpgradeItemData_SO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public int valueAmount;
    public PlayerProperty effectProperty;
}