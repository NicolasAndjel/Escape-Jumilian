using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    public float life;
    public float damage;
    public float timeStunt;
    public GameObject stuntUI;
    public GameObject bullet;
    public Damaggeable target;
    public EnemiesMovement em;
    public Animator anim;
    public bool attacking;
    public bool hitted;
    public bool stunt;
    public float tick;
    public float timeBtwHits;
    float timer;
    bool done;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeStunt;
        anim = GetComponent<Animator>();
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
                anim.SetBool("Attacking", true);
                target.GetDamage(damage);
                tick = 0;
            }
            if (anim.GetBool("Attacking"))
                anim.SetBool("Attacking", false);
        }

        tick += Time.deltaTime;

    }

    private void GetStunt()
    {
        if (stunt)
        {
            if (!done)
            {
                anim.SetBool("Attacked", false);
                Instantiate(stuntUI, transform.position, Quaternion.identity);
                done = true;
            }
            if (timer > 0)
            {
                em.speed = 0;                
            }
            else stunt = false;
            timer -= Time.deltaTime;
        }
        else
        {
            done = false;
            em.speed = em.saveSpeed;
            timer = timeStunt;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            stunt = true;
            anim.SetBool("Attacked", true);
        }
    }
}
