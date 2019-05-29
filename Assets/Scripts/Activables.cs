﻿using System.Collections;
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
                pads[i] = interruptors[i].transform.GetChild(0).GetComponent<Interruptor>();
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
                if (!pads[i].active)
                {
                    count = 0;
                }
                else if (pads[i].active)
                {
                    count++;
                }
            }

            if (count == pads.Length)
            {
                active = true;
            }
            else active = false;
        }
        else if (panel.IsActive())
        {
            active = true;
        }
        
    }
}
