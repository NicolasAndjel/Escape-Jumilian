using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public GameObject pad;
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
        direction = stop.position - pad.transform.position;
        direction.z = 0;
        speed = direction.magnitude / time;
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(stop.position, pad.transform.position) > 0.1)
        {
            ActivatePad(close);
            DeactivatePad(close);
        }
        else
        {
            open = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerToGetActivated)
        {
            close = false;            
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            close = true;
        }
    }

    public void InstantOpen()
    {
        pad.transform.position = stop.transform.position;
    }

    private void ActivatePad(bool close)
    {
        if (!close)
        {
            if (!open)
            {
                pad.transform.position += direction.normalized * speed * Time.deltaTime;
                if (Vector3.Distance(transform.position, direction) < 0.01 && !open) open = !open;
            }
        }

    }

    private void DeactivatePad(bool close)
    {
        if (close && Vector3.Distance(closed.position, pad.transform.position) > 0.01)
        {
            pad.transform.position += direction.normalized * -speed * Time.deltaTime;
        }
    }
}