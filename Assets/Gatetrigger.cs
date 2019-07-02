using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatetrigger : MonoBehaviour
{
    public BoxCollider2D bc;
    public Animator anim;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        
        if (collision.gameObject.layer == 8)
        {
            
            GetComponent<Animator>().Play("Move");
            Destroy(this);
            Destroy(bc);
        }        
    }
}
