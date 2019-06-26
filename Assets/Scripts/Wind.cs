using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float forceAmount;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 19)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * forceAmount, ForceMode2D.Impulse);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * forceAmount * 100, ForceMode2D.Impulse);
        }
    }
}
