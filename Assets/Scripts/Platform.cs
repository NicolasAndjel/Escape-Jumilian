using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Activables
{     
    public float speed;  
    public Transform[] waypoints;
    public int currentWp;
    public int direction;
    public bool on;
    

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        on = true;
        direction = 1;        
	}

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();
        Move();       
	}

    public void Move()
    {
        if (active)
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
}
