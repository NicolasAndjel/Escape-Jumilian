using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    bool done;
    [HideInInspector]
    public Vector3 force;
    public float forceStrenght;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.parent.gameObject, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!done)
        {
            collision.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
            done = true;
        }
    }
}
