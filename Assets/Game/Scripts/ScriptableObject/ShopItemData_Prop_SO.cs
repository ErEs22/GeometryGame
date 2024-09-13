using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ShopProp",fileName = "New ShopItem_Prop")]
public class ShopItemData_Prop_SO : ShopItemData_SO
{
    //数值计算模式为百分比模式
    public List<ShopPropPropertyPair> itemProperties = new List<ShopPropPropertyPair>();
}