using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public bool activatedByPlasma;
    public bool isWorking;
    public bool active;
    public AudioSource aS;
    public SpriteRenderer sr;    
    public Sprite[] sprites;
    bool done;
    
    void Start()
    {
        if (activatedByPlasma)
            isWorking = false;
        else isWorking = true;
        aS = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        IsWorkingUI();
    }

    private void Update()
    {
        IsWorkingUI();
        if (isWorking)
        ActiveUI(active);       
    }

    private void IsWorkingUI()
    {
        if (isWorking)
        {
            
            sr.sprite = sprites[1];
        }
        else
        {       
            sr.sprite = sprites[0];           
        }
    }

    private void ActiveUI(bool on)
    {
        if (on)
        {
            if (!done)
            {
                aS.Play();
                done = true;
            }
            sr.sprite = sprites[2];
        }
        else
        {
            done = false;
            sr.sprite = sprites[1];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if (isWorking)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
            {
                foreach (ContactPoint2D hitPos in collision.contacts)
                {
                    if (hitPos.normal.y < -0.5)
                    {
                        active = true;
                    }
                }
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activatedByPlasma)
        {
            if (collision.gameObject.layer == 18)
            {
                isWorking = true;
                activatedByPlasma = false;
            }    
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isWorking)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
            {
                active = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isWorking)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
            {
                active = false;
            }
        }
    }

}