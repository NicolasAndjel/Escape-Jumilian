using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{
    public Damaggeable dm;
    public PlayersHabilities ph;
    public Transform otherPlayerDm;
    public PlayersHabilities pho;
    public GameObject bullet;
    public float distance;
    float delay;
    public float timeBtwBullets;

    // Start is called before the first frame update
    public virtual void Start()
    {
        delay = timeBtwBullets;
        ph = GetComponent<PlayersHabilities>();
        dm = GetComponent<Damaggeable>();
        pho = otherPlayerDm.gameObject.GetComponent<PlayersHabilities>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        PowerOverTime();
        Refill();
        ShootElement();
    }

    public void PowerOverTime()
    {
        dm.health -= Time.deltaTime;
    }

    public void Refill()
    {
        distance = Vector3.Distance(otherPlayerDm.position, transform.position);
        if (distance < 0.3)
        {
            if (Input.GetButton(ph.useButton) && (Input.GetButton(pho.useButton)))
            {
                dm.health = dm.maxH;
            }
        }
    }

    public void ShootElement()
    {
        if (Input.GetButton(ph.elementButton))
        {
            
            SpawnBullets(ph.GetBulletSpawn());
            delay += Time.deltaTime;
        }
        if (Input.GetButtonUp(ph.elementButton))
        {
            delay = timeBtwBullets;
        }
    }

    public void SpawnBullets(Transform bulletSpawn)
    {
        
        if (delay >= timeBtwBullets)
        {
            dm.health = dm.health -1;
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            bulletScript.damage = ph.elementalDamage;
            bulletScript.direction = ph.pm.GetFacing();
            bulletScript.time = 3;
            delay = 0;
        }
        
    }

}
