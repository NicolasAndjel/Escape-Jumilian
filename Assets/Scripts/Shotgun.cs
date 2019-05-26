using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    [Range(0, 1)]
    public float time;   
    public List<Transform> spawns;

    public override void Start()
    {
        base.Start();        
        for (int i = 0; i < transform.GetChild(0).gameObject.transform.childCount; i++)
        {
            spawns.Add(transform.GetChild(0).GetChild(i));
        }
    }

    public override void Update()
    {
        base.Update();
        timeBtwBullets = time;
    }

    public override void Use(Vector3 facing)
    {
        if (delay < 0)
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                var tempBullet = Instantiate(bullet, spawns[i].position, spawns[i].rotation);
                var bulletScript = tempBullet.GetComponent<Bullet>();
                bulletScript.direction = facing;
                bulletScript.time = 1.5f;
                delay = timeBtwBullets;
            }                

        }
        delay -= Time.deltaTime;
    }
    
}
