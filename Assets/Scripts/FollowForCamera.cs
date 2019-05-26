using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowForCamera : MonoBehaviour
{
    public Transform[] chars;
    bool doFollow;
    float z;
    public Animator anim;   
    // Start is called before the first frame update    

    private void Start()
    {
        doFollow = true;
        z = transform.position.z;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame

    void Update()
    {
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == null)
            {
                doFollow = false;
                return;
            }
        }

        if (doFollow)
        {
            Vector3 position = Vector3.Lerp(chars[0].position, chars[1].position, 0.7f);
            position.z = z;
            transform.position = position;
        }
               
        
    }
    
}
