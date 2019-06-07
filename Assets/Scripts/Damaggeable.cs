using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damaggeable : MonoBehaviour
{
    public enum States
    {
        NORMAL,
        GODMODE,
    }
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator anim;
    public SpriteRenderer sr;
    public float health;       
    public Scrollbar lifeUI;
    public float layerToGetDamage;
    public float layerToGetDamge2;
    public Color ColorWhenDamaged;
    Color startColor;
    public float instaKill;
    public States currentState;
    bool didColored;
    bool dead;
    public float maxH;
    bool enemie;
    float timer;
    float deadTime;

    private void Start()
    {
        if (GetComponent<EnemiesMovement>() != null)
        {
            enemie = true;
        }
        else enemie = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        didColored = true;
        maxH = health;
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
        currentState = States.NORMAL;
    }

    void Update()
    {
        if (!dead)
            HealthUI();
        else DeSpawn();
        if (MustDie())
        {
            Die();
        }                
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerToGetDamage || collision.gameObject.layer == layerToGetDamge2 || collision.gameObject.layer == 11)
        {
            GetDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }        
    }    

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerToGetDamage)
        {            
            sr.color = startColor;
        }
    }

    public void GetDamage(float amount)
    {        
        health -= amount;
        sr.color = ColorWhenDamaged;
        didColored = false;        
    }

    private void DeSpawn()
    {
        Destroy(gameObject, 2);
    }

    public void Die()
    {
        if (!enemie)
        {
            lifeUI.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            GetComponent<HerosMovement>().speed = 0;
        }
        else
        {
            GetComponent<EnemiesMovement>().active = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        bc.enabled = false;        
        anim.Play("Die");
        dead = true;       
        sr.color = startColor;
    }

    private void HealthUI()
    {
        if (!enemie)
        {
            lifeUI.size = health / maxH;
        }
        if (!didColored)
        {
            if (!enemie)
            {
                if (timer > 0.5)
                {
                    sr.color = startColor;
                    didColored = true;
                    timer = 0;
                }
                timer += Time.deltaTime;
            }
            else
            {
                if (timer > 1)
                {
                    sr.color = startColor;
                    didColored = true;
                    timer = 0.6f;
                }
                timer += Time.deltaTime;
            }

        }
    }
    

    private bool MustDie()
    {
        if (health > 0)
        {
            return false;
        }
        else return true;
    }

}