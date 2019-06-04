using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : PlayerElement
{

    public GameObject airExplotion;
    public float timeBtwAirs;
    float tim;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        tim = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        PowerOverTime();
        Refill();
        SpecialAttack();
    }


    void SpecialAttack()
    {
        if (Input.GetButtonDown(ph.elementButton) && tim > timeBtwAirs)
        {
            GameObject tempAir = Instantiate(airExplotion, ph.GetBulletSpawn().position, Quaternion.identity);
            var tempScript = tempAir.transform.GetChild(0).GetComponent<AirForce>();
            tempScript.force = ph.hm.GetFacing();
            tim = 0;
        }
        tim += Time.deltaTime;
    }

    
}

