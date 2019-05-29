using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalActivator : MonoBehaviour
{
    public float amountToFill;
    float amount = 0;
    public bool filled;
    

    // Update is called once per frame
    void Update()
    {
        if (amount > amountToFill)
        {
            filled = true;
        }
    }
}
