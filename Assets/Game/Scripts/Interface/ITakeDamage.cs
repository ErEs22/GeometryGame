using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受伤接口，所有会受伤的对象都应当继承这个接口
/// </summary>
public interface ITakeDamage
{
    /// <summary>
    /// 玩家和敌人受伤接口方法
    /// </summary>
    /// <param name="damage">受伤伤害</param>
    /// <param name="isCritical">是否暴击</param>
    /// <returns>受伤后是否死亡</returns>
    public bool TakeDamage(int damage,bool isCritical = false);
}
