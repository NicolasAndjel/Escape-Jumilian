using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public bool active;
    public float time;   
    
    void Start()
    {
        if (active)
        Destroy(gameObject, time);
    }
   
}
