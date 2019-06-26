using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByElement : MonoBehaviour
{
    public Panels[] ea;
    public float counter;
    public Animator anim;
    public bool active;


    void Update()
    {
        counter = 0;
        for (int i = 0; i < ea.Length; i++)
        {
            if (ea[i].active)
            {
                counter++;
            }
            else return;
        }
        if (counter == ea.Length)
        {
            active = true;           
        }
        else active = false;

        if (active)
        {
            anim.Play("Lift");
        }
        else anim.Play("Close");
    }
}
