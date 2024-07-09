using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Cannon : Projectile
{
    float damageRange = 5f;

    protected override void Hit(Collider2D hitObject)
    {
        base.Hit(hitObject);
    }
}
