using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon")]
public class WeaponData_SO : ScriptableObject
{
    public GameObject projectile;
    public int baseDamage = 10;
    public float fireInterval = 0.1f;
    [Range(0f,20f)]
    public float range = 10f;
    public int projectileSpeed = 20;
}
