using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Activables
{
    public Transform[] points;
    public int currentPoint;
    public float speed;  
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (active)
        {
            Open();
        }
        else Close();
        Move();
    }

    private void Open()
    {
        currentPoint = 1;
    }

    private void Close()
    {
        currentPoint = 0;
    }

    public void Move()
    {
        Vector3 direction = points[currentPoint].position - transform.position;

        if (currentPoint == 1)
        {
            if (direction.y > speed * Time.deltaTime)
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
            else transform.position = points[currentPoint].position;
        }
        else if (currentPoint == 0)
        {
            if (direction.y > -speed * Time.deltaTime)
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
            else transform.position = points[currentPoint].position;
        }
    }    
}
