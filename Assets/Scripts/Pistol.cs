using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    [Range(0, 1)]
    public float time;

    public override void Update()
    {
        base.Update();
        timeBtwBullets = time;
    }
}
