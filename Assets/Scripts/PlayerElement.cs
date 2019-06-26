using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public GameObject[] costUi;
    public Transform[] costUiTransform;
    public GameObject healingUi;
    public GameObject muzzle;
    public Damaggeable dm;
    public PlayersHabilities ph;
    public Transform otherPlayerDm;
    public PlayersHabilities pho;
    public GameObject bullet;
    public GameObject pointer;
    public float distance;
    public float minDistance;
    public float minDistanceSave;
    Vector2 otherpjdir;
    float delay;
    public float timeBtwBullets;
    float timer;
    float timerUi;

    // Start is called before the first frame update
    public virtual void Start()
    {
        minDistanceSave = minDistance;
        timer = 0;
        delay = timeBtwBullets;
        ph = GetComponent<PlayersHabilities>();
        dm = GetComponent<Damaggeable>();
        pho = otherPlayerDm.gameObject.GetComponent<PlayersHabilities>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if ((dm.health < (dm.maxH * 0.3)) && ((distance > 2) && pho != null))
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
        ShootElement();
    }

    public void PointToAlly()
    {
        otherpjdir = pho.transform.position - transform.position;
        float angle = Mathf.Atan2(otherpjdir.y, otherpjdir.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        pointer.transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 1);
    }

    public void PowerOverTime()
    {
        dm.health -= Time.deltaTime;
    }

    public void Refill()
    {
        if (otherPlayerDm == null)
        {
            return;
        }
        distance = Vector3.Distance(otherPlayerDm.position, transform.position); 
        if (distance < minDistance)
        {
            if (Input.GetButton(ph.useButton) && (Input.GetButton(pho.useButton)))
            {
                dm.uiIimer = 4;
                healingUi.SetActive(true);
                if (timer > 0.3)
                {
                    Instantiate(costUi[1], GetCostSpawn().position, Quaternion.identity, transform);
                    dm.health += dm.maxH / 10;
                    timer = 0;
                    if (dm.health > dm.maxH)
                    {
                        dm.health = dm.maxH;
                    }
                }
                timer += Time.deltaTime;
            }
            else healingUi.SetActive(false);
        }
    }

    public Transform GetCostSpawn()
    {
        Transform newtr = null;
        if (ph.hm.GetFacing() == Vector3.left)
        {
            newtr = costUiTransform[0];
        }
        else newtr = costUiTransform[1];
        return newtr;
    }

    public void ShootElement()
    {

        if (Input.GetButton(ph.elementButton) && dm.health > 12)
        {
            SpawnBullets(ph.GetBulletSpawn());
            delay += Time.deltaTime;
        }
        if (Input.GetButtonUp(ph.elementButton))
        {
            ph.hm.anim.SetBool("IsShooting", false);
        }
    }

    public void SpawnBullets(Transform bulletSpawn)
    {
        if (delay >= timeBtwBullets)
        {
            Instantiate(costUi[0], GetCostSpawn().position, Quaternion.identity, transform);
            if (muzzle != null)
                Instantiate(muzzle, bulletSpawn.position, Quaternion.identity);
            dm.health = dm.health - 10;
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            bulletScript.damage = ph.elementalDamage;
            bulletScript.direction = ph.hm.GetFacing();
            bulletScript.time = 3;
            delay = 0;
            ph.hm.anim.SetBool("IsShooting", true);
        }
    }
}
   
