using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour
{
    public GameObject door;
    public Transform stop;
    public Transform closed;
    public float LayerToGetActivated;
    public bool open;
    private bool close;
    Vector3 direction;
    public float time;
    float speed;

    // Start is called before the first frame update

    void Start()
    {
        direction = stop.position - door.transform.position;
        direction.z = 0;
        speed = direction.magnitude / time;
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(stop.position, door.transform.position) > 0.1)
        {
            OpenDoor(close);
            CloseDoor(close);            
        }
        else
        {
            open = true;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerToGetActivated)
        {
            close = false;        
            InstantOpen();
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            close = true;
        }
    }

    public void InstantOpen()
    {
        door.transform.position = stop.transform.position;
    }

    private void OpenDoor(bool close)
    {
        if (!close)
        {
            if (!open)
            {
                door.transform.position += direction.normalized * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, direction) < 0.01 && !open) open = !open;
            }
        }
            
    }

    private void CloseDoor(bool close)
    {
        if (close && Vector3.Distance(closed.position, door.transform.position) > 0.01)
        {
            door.transform.position += direction.normalized * -speed * Time.deltaTime;
        }
    }
}
