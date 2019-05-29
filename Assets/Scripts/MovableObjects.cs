using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 10)
        {
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
