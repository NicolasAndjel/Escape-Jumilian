using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    bool done = false;   
    public Vector3 force;
    public float forceStrenght;
    // Start is called before the first frame update
    void Start()
    {
        if (force.x < 0)
        {
            transform.localScale = transform.localScale * -1;
        }
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!done)
        {
            if (collision.attachedRigidbody)
            {
                collision.attachedRigidbody.AddForce(force * forceStrenght, ForceMode2D.Impulse);
                done = true;
            }            
        }
    }
}
