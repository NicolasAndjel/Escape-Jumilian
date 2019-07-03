﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public ActivateByOxygen abo;
    public ActivateByElement abe;
    public SpriteRenderer pj;
    public Animator anim;
    AudioSource aS;
    bool done;
    public void Awake()
    {
        aS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {       
        if (abe.active && abo.active)
        {
            anim.Play("Lift");
            if (!done)
            {
                aS.Play();
                done = true;
            }
        }
        else anim.Play("Close");
        
        if (abo.active && abe.active && pj != null)
        {
            pj.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            pj = collision.gameObject.GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) pj = null;
    }
}
