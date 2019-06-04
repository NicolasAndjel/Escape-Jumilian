using System.Collections;
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
        _delay += Time.deltaTime;
    }

    public void SpawnBullets(Transform bulletSpawn)
    {
        if (_delay > timeBtwBullets)
        {
            muzzle.transform.position = bulletSpawn.position;
            muzzle.SetActive(true);
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);          
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            bulletScript.damage = damage;
            bulletScript.direction = hm.GetFacing();
            bulletScript.time = 10;
            _delay = 0;
        }
        if (muzzle.activeSelf == true && _delay > 0.3)
        {
            muzzle.SetActive(false);
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
