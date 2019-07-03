using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource aS;
    bool done;
    float timer;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (done)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                done = false;
                timer = 0;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 10)
        {
            if (!done)
            {
                aS.Play();
                done = true;
            }
            transform.SetParent(collision.gameObject.transform);            
        }        
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 10)
        {
            transform.SetParent(null);
        }
    }
}
