using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoBrain : Platform
{

    public enum States
    {
        PATROL,
        ATTACK
    }
    public GameObject poof;
    public float life;
    public States currentState;
    public Damaggeable currentTarget;
    public float damage;
    public Spawner sp;
    public float timer;
    public float timeToAttack;
    public SpriteRenderer sr;

    public override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        currentState = States.PATROL;
        on = true;
        direction = 1;
    }

    public override void Update()
    {       
    	 
        if (currentState == States.PATROL)
        {
            base.Update();
            Move();
        }            
        else Attack();
    }

    public void Attack()
    {
        transform.position += (transform.position - currentTarget.transform.position).normalized * speed * Time.deltaTime;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Damaggeable>().GetDamage(damage);
            Instantiate(poof, transform.position, Quaternion.identity);
            sp.counter--;
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 25)
        {
            Instantiate(poof, transform.position, Quaternion.identity);
            sp.counter--;
            Destroy(gameObject);
        } 
        else if (collision.gameObject.layer == 18)
        {
            Instantiate(poof, transform.position, Quaternion.identity);
            sp.counter--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (currentTarget != null)
            {
                currentState = States.ATTACK;
                currentTarget = collision.gameObject.GetComponent<Damaggeable>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            currentState = States.PATROL;
            currentTarget = null;
        }
       
    }
  
}
