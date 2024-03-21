using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData_SO : ScriptableObject
{
    public GameObject projectile;
    public int baseDamage = 10;
    public float fireInterval = 0.1f;
}
