using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHabilities : MonoBehaviour
{
    public float life;
    public float damage;
    public float timeStunt;
    public GameObject bullet;
    public Damaggeable target;
    public EnemiesMovement em;
    public bool hitted;
    bool done;
    public bool stunt;
    public float tick;
    public float timeBtwHits;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeStunt;
        em = GetComponent<EnemiesMovement>();        
    }

    // Update is called once per frame
    void Update()
    {
        GetStunt();
        DoDamage();       
        hitted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            target = collision.gameObject.GetComponent<Damaggeable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            em.speed = em.saveSpeed;
            target = null;
        }
    }

    private void DoDamage()
    {
        if (target != null)
        {
            em.speed = 0;
            if (timeBtwHits < tick)
            {
                target.GetDamage(damage);
                tick = 0;
            }
            tick += Time.deltaTime;

        }
        else tick = 0;
    }

    private void GetStunt()
    {
        if (stunt)
        {
            if (timer > 0)
            {
                em.speed = 0;
            }
            else stunt = false;
            timer -= Time.deltaTime;
        }
        else
        {
            em.speed = em.saveSpeed;
            timer = timeStunt;            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            stunt = true;
        }
    }


}
