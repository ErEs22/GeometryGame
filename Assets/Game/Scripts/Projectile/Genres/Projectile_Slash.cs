using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Slash : Projectile
{

    private void OnEnable() {
        lifeTime = 0.5f;
    }
    protected override void Fly()
    {
    }
}
