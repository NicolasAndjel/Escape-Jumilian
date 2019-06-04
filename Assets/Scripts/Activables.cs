using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activables : MonoBehaviour
{

    public bool UseButtonsOnly;
    public Panels panel;
    public GameObject[] interruptors;
    public Interruptor[] pads;
    public bool active;
    private float count;

    public virtual void Start()
    {
        if (UseButtonsOnly)
        {
            count = 0;
            pads = new Interruptor[interruptors.Length];
            for (int i = 0; i < pads.Length; i++)
            {
                pads[i] = interruptors[i].GetComponent<Interruptor>();
            }
            interruptors = null;
        }       
    }


    public virtual void Update()
    {        
        if (UseButtonsOnly)
        {
            for (int i = 0; i < pads.Length; i++)
            {
                if (pads[i].active)
                {
                    count = count + 1;
                }                
            }
            if (count == pads.Length)
            {
                active = true;
            }
            else active = false;
            count = 0;
        }
        else if (panel.IsActive())
        {
            active = true;
        }      
        
    }
}
