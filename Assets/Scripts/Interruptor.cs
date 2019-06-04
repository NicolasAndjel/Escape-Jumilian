﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public bool active;
    public SpriteRenderer sr;
    //private Rigidbody2D rb;
    public Sprite[] sprites;   

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
       //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ActiveUI(active);       
    }

    private void ActiveUI(bool on)
    {
        if (on)
        {
            if (sr.sprite != sprites[0])
            sr.sprite = sprites[0];
        }
        else
        {
            if (sr.sprite != sprites[1])
            sr.sprite = sprites[1];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                Debug.Log(hitPos.normal);
                if (hitPos.normal.y < -0.5)
                {
                    active = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
        {
            active = false;
        }
    }


}