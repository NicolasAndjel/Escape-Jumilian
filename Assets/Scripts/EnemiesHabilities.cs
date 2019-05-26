using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHabilities : MonoBehaviour
{
    public float life;
    public float damage;
    public GameObject bullet;
    public Damaggeable target;
    public EnemiesMovement em;
    public bool hitted;
    bool done;
    public float tick;
    public float timeBtwHits;

    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<EnemiesMovement>();
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
   

}
