using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPlayerHabilities : PlayersHabilities
{

    public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
   

    public override Transform GetBulletSpawn()
    {       
        Transform a = null;
        if (hm.GetFacing() == Vector3.left)
        {
            a = bulletSpawnLeft;
        }
        else
        {
            a = bulletSpawnRight;
        }
        return a;
    }
}

   
