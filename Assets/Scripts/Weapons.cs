using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{    
    private Rigidbody2D rb;
    public Transform bulletSpawn;
    public GameObject bullet;   
    public float timeBtwBullets;
    public float delay;
    bool done;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        SetGravity();
        delay += Time.deltaTime;
    }    

    public void Initialization()
    {
        done = false;
        rb = GetComponent<Rigidbody2D>();
        bulletSpawn = transform.GetChild(0).GetChild(0);
    }

    public void PickedUp(Transform here, Vector3 facing)
    {
        transform.parent = here;
        transform.position = here.position;
        
        if (facing == Vector3.left)
        {            
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }
        done = !done;
        
    }

    public virtual void Use(Vector3 facing)
    {
        if (delay > timeBtwBullets)
        {            
            GameObject tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Debug.Log("Sucedo en cualquier lado");
            Bullet bulletScript = tempBullet.GetComponent<Bullet>();
            bulletScript.direction = facing;
            bulletScript.time = 3;
            delay = 0;
        }        
    }

    private void SetGravity()
    {
        if (transform.parent != null)
        {
            rb.isKinematic = true;
        }
        else if (rb.isKinematic = true && transform.parent == null)
        {
            rb.isKinematic = false;
        }
    }
}
