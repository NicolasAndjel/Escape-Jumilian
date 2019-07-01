using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinta : MonoBehaviour
{
    public bool right;
    public List<Rigidbody2D> currentHero;
    public float force;
   
    void Update()
    {
        if (right)
        {
            for (int i = 0; i < currentHero.Count; i++)
            {
                currentHero[i].AddForce(transform.right * force, ForceMode2D.Impulse);             
            }
        }
        else
        {
            for (int i = 0; i < currentHero.Count; i++)
            {
                currentHero[i].AddForce(-transform.right * force, ForceMode2D.Impulse);          
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!currentHero.Contains(collision.rigidbody) && collision.gameObject.layer == 8)
        {
            currentHero.Add(collision.rigidbody);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentHero.Contains(collision.rigidbody) && collision.gameObject.layer == 8)
        {
            currentHero.Remove(collision.rigidbody);
        }
    }
}
