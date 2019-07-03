using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateByTime : MonoBehaviour
{
    public bool isPanel;
    public Activables ac;
    public ElementalActivator ea;
    public float timer;
    public float timeTodo;


    private void Start()
    {
        if (isPanel)
        {
            ac = GetComponent<Panels>();
        }
        else ea = GetComponent<ElementalActivator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPanel)
        {
            if (!ac.active && ea.filled)
            {
                timer += Time.deltaTime;
                if (timer > timeTodo)
                {
                    ea.filled = false;
                    ea.amountToFill = 13;
                    timer = 0;
                }
            }
            else if (ac.active && ea.filled)
                timer = 0;
        }
        else
        {
            if (!ea.filled && ac.active)
            {
                timer += Time.deltaTime;
                if (timer > timeTodo)
                {
                    ac.active= false;
                    timer = 0;
                }
            }
            else if (ea.filled && ac.active)
                 timer = 0;
        }
        
    }
}
