using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受伤接口，所有会受伤的对象都应当继承这个接口
/// </summary>
public interface ITakeDamage
{
    public bool TakeDamage(int damage);
}
