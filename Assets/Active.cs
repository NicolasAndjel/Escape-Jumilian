using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public bool active;
    public Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        anim.SetBool("Active", active);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        active = true;
    }
}
