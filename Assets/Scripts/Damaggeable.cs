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
    public SpriteRenderer sr;
    public float health;       
    public Scrollbar lifeUI;
    public float layerToGetDamage;
    public Color ColorWhenDamaged;
    Color startColor;
    public float instaKill;
    public States currentState;
    bool didColored;
    public float maxH;
    bool enemie;
    float timer;

    private void Start()
    {
        if (GetComponent<EnemiesMovement>() != null)
        {
            enemie = true;
        }
        else enemie = false;
        didColored = true;
        maxH = health;
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
        currentState = States.NORMAL;
    }

    void Update()
    {
        HealthUI();        
        if (MustDie())
        {
            Die();
        }
                
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {     
        if (collision.gameObject.layer == layerToGetDamage || collision.gameObject.layer == 11)
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

    public void Die()
    {
        if (!enemie)
        {
            lifeUI.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        Destroy(gameObject);                               
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
                if (timer > 1)
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