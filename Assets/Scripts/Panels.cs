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
        
    }

    public bool IsActive()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
