using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    bool done = false;   
    public Vector3 force;
    public float forceStrenght;
    Animator anim;
    public float time;
   
    void Start()
    {
        time = 1;
        anim = GetComponent<Animator>();        
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;    
        for (int i = 0; i < ac.animationClips.Length; i++)
        {   
            if (ac.animationClips[i].name == "Oxygen Special")     
            {
                time = ac.animationClips[i].length;
            }
        }
        anim = null;
        if (force.x < 0)
        {
            transform.parent.localScale = transform.localScale * -1;
        }
        Destroy(gameObject.transform.parent.gameObject, time);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!done)
        {
            if (collision.attachedRigidbody)
            {
                collision.attachedRigidbody.AddForce(force * forceStrenght, ForceMode2D.Impulse);
                done = true;
            }            
        }
    }
}
