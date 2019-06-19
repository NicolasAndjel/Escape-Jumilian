using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 10)
        {
            transform.SetParent(collision.gameObject.transform);            
        }
        if (collision.gameObject.layer == 14)
        {
            rb.AddForce(-rb.velocity * 500, ForceMode2D.Impulse);
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
