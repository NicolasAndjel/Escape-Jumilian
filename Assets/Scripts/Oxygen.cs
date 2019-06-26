using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : PlayerElement
{

    public GameObject airExplotion;
    public float timeBtwAirs;
    public float OxygenCost;
    float tim;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        tim = 0;
        ph = GetComponent<OxygenPlayerHabilities>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (dm.health < (dm.maxH * 0.3) && (distance > 2))
        {
            if (!pointer.activeInHierarchy)
                pointer.SetActive(true);
            PointToAlly();
        }
        else
        {
            if (pointer.activeInHierarchy)
            {
                pointer.SetActive(false);
            }
        }
        PowerOverTime();
        Refill();
        SpecialAttack();
    }


    void SpecialAttack()
    {
        if (Input.GetButtonDown(ph.elementButton) && tim > timeBtwAirs && dm.health > (OxygenCost + 5))
        {
            Instantiate(costUi[0], GetCostSpawn().position, Quaternion.identity, transform);
            GameObject tempAir = Instantiate(airExplotion, ph.GetBulletSpawn().position, Quaternion.identity);     
            AirForce tempScript = tempAir.transform.GetChild(0).GetComponent<AirForce>();
            tempScript.force = ph.hm.GetFacing();
            tim = 0;
            ph.hm.anim.SetBool("IsShooting", true);
            GetComponent<Damaggeable>().health -= OxygenCost;
        }
        else if(Input.GetButtonUp(ph.elementButton)) ph.hm.anim.SetBool("IsShooting", false);
        tim += Time.deltaTime;
    }    
}

