using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public bool active;
    public float time;
    public AudioSource aS;
    
    void Start()
    {
        aS = GetComponent<AudioSource>();
        if (aS == null)
        {
            if (active)
                Destroy(gameObject, time);
        }
        else
        {
            if (!aS.isPlaying)
            {
                Destroy(gameObject);
            }
        }
       
    }
   
}
