using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : PlayerElement
{

    public GameObject airExplotion;
    public float timeBtwAirs;
    float timer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        timer = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        SpecialAttack();
    }


    void SpecialAttack()
    {
        if (Input.GetButtonDown(ph.pickUpButton) && timer > timeBtwAirs)
        {
            GameObject tempAir = Instantiate(airExplotion, ph.GetBulletSpawn().position, Quaternion.identity);
            var tempScript = tempAir.transform.GetChild(0).GetComponent<AirForce>();
            tempScript.force = ph.hm.GetFacing();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    
}

