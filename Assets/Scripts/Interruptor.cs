using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public bool active;
    private Rigidbody2D rb;
    public Sprite[] sprites;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (active)
        {
            transform.parent.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else transform.parent.GetComponent<SpriteRenderer>().sprite = sprites[1];
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active && collision.gameObject.layer == 8)
        rb.gravityScale = 1;
        if (collision.gameObject.layer == 4)
        {
            active = true;            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
            rb.gravityScale = -1;
        if (collision.gameObject.layer == 4)
        {
            active = false;
        }
    }


}