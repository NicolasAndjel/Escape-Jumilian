using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public bool active;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        active = true;
    }
}
