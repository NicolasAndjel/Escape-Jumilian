using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHabilities : MonoBehaviour
{     
    public GameObject bullet;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;  
    public GameObject currentLever;
    public PlayersMovement pm;
    public string fireButton;
    public string pickUpButton;
    public string elementButton;
    public string useButton;
    bool lever;
    float delay;
    public float timeBtwBullets;
    public bool hasWeapon;
    public float j;
    public float elementalDamage;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {        
        pm = GetComponent<PlayersMovement>();
        fireButton += pm.player;
        pickUpButton += pm.player;
        elementButton += pm.player;
        useButton += pm.player;
        delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        j = Vector3.left.magnitude;
        UseLever();
        Shoot(GetBulletSpawn());            
    }
   
    public Transform GetBulletSpawn()
    {
        Transform a = null;
        if (pm.GetFacing() == Vector3.left)
        {
            a = bulletSpawnLeft;
        }
        else
        {            
            a = bulletSpawnRight;            
        }
        return a;
    }

    
    private void UseLever()
    {
        if (lever)
        {
            if (Input.GetButtonDown(pickUpButton))
            {
                currentLever.gameObject.GetComponent<Panels>().InstantOpen();
            }
        }
       
    }

    private void Shoot(Transform spawn)
    {   
        if (Input.GetButton(fireButton))
        {
            SpawnBullets(spawn);
        }
        delay += Time.deltaTime;
    }

    public void SpawnBullets(Transform bulletSpawn)
    {
        if (delay > timeBtwBullets)
        {
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);          
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            bulletScript.damage = damage;
            bulletScript.direction = pm.GetFacing();
            bulletScript.time = 3;
            delay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            lever = true;
            currentLever = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            lever = false;
            currentLever = null;
        }
    }


    /*private void InteractWithItems()
    {
        if (Input.GetButton(pickUpButton + pm.player))
        {
            switchWeaponDelay += Time.deltaTime;
        }
        else switchWeaponDelay = 0;

        if (!hasWeapon)
        {
            if (Input.GetButtonDown(pickUpButton + pm.player))
            {
                PickUpWeapon();
            }
        }
        else if (hasWeapon)
        {
            if (switchWeaponDelay > 0.4 && Input.GetButton(pickUpButton + pm.player))
            {
                DropWeapon();
                PickUpWeapon();
            }
        }      
           
        
    }

   

    private void DropWeapon()
    {
        hasWeapon = false;
        if (transform.childCount > 0)
        {
            transform.GetChild(0).parent = null;
        }
    }

    private void PickUpWeapon()
    {
        if (pickeableWeapon != null)
        {
            hasWeapon = true;
            currentWeapon = pickeableWeapon.GetComponent<Weapons>();
            pickeableWeapon = null;
            currentWeapon.PickedUp(transform, pm.facing);
        }
    }*/
}
