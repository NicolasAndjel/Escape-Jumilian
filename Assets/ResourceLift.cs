using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLift : MonoBehaviour
{

    public ResourceLift rs;
    public bool active;
    public bool ohterIsActive;
    public bool wifi;
    public PlayerElement pe;
    public Oxygen pe2;


    private void Update()
    {
        rs.ohterIsActive = active;
        if (ohterIsActive && active)
        {
            wifi = true;
        }
        else wifi = false;

        if (wifi)
        {
            pe.minDistance = 10000;
            pe2.minDistance = 10000;
        }
        else
        {
            pe.minDistance = pe.minDistanceSave;
            pe2.minDistance = pe.minDistance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            active = false;
        }
    }
}
