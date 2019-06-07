﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHabilities : MonoBehaviour
{
    public GameObject muzzle;
    public GameObject bullet;    
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;  
    public GameObject currentLever;
    public HerosMovement hm;
    public string fireButton;
    public string pickUpButton;
    public string elementButton;
    public string useButton;
    float _delay;
    public float timeBtwBullets;      
    public float elementalDamage;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {        
        hm = GetComponent<HerosMovement>();
        fireButton += hm.player;
        pickUpButton += hm.player;
        elementButton += hm.player;
        useButton += hm.player;       
        _delay = 0;
    }

    // Update is called once per frame
    void Update()
    {        
        Shoot(GetBulletSpawn());            
    }
   
    public Transform GetBulletSpawn()
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

    private void Shoot(Transform spawn)
    {   
        if (Input.GetButton(fireButton))
        {
            SpawnBullets(spawn);
        }
        if (Input.GetButtonUp(fireButton))
        {
            hm.anim.SetBool("IsShooting", false);
        }       
        _delay += Time.deltaTime;
    }

    public void SpawnBullets(Transform bulletSpawn)
    {
        if (_delay > timeBtwBullets)
        {
            Instantiate(muzzle, bulletSpawn.position, Quaternion.identity);
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            //bulletScript.transform.rotation = Quaternion.Euler(0, 90, 0);
            bulletScript.damage = damage;
            bulletScript.direction = hm.GetFacing();
            bulletScript.time = 10;
            _delay = 0;
            hm.anim.SetBool("IsShooting", true);
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            currentLever = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            currentLever = null;
        }
    }   
  
}
