using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{     
    public float speed;  
    public Transform[] waypoints;
    public int currentWp;
    public int direction;
    
    

	// Use this for initialization
	public virtual void Start ()
    {        
        direction = 1;        
	}

    // Update is called once per frame
    public virtual void Update ()
    {
        Move();       
	}

    public void Move()
    {
        Vector3 distance = waypoints[currentWp].position - transform.position;

        if (distance.magnitude > speed * Time.deltaTime)
        {
            transform.position += distance.normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position = waypoints[currentWp].position;
            currentWp += direction;

            if (currentWp >= waypoints.Length || currentWp < 0)
            {                
                direction *= -1;
                currentWp += direction;
            }
        }
    }

    
}
