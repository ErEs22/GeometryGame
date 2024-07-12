using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Slash : Projectile
{

    private void OnEnable() {
        lifeTime = 1f;
    }
    protected override void Fly()
    {
    }
}
