using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public ActivateByOxygen abo;
    public ActivateByElement abe;
    public SpriteRenderer pj;


    public void Update()
    {
        if (abo != null)
        {
            if (abo.active && pj != null)
            {
                pj.gameObject.SetActive(false);
            }
        }
        else if (abe != null)
        {
            if (abe.active && pj != null)
            {
                pj.gameObject.SetActive(false);
            }
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
