using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : Activables
{    

    public override void Start()
    {
        
    }

    public override void Update()
    {

    }

    public bool IsActive()
    {
        return active;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            active = true;
        }
    }
}
