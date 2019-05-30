using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float tick;
    public int i;
    public GameObject enemy;
    public List<EnemiesHabilities> aliens;

    // Start is called before the first frame update
    void Start()
    {        
        i = 0;
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {        

        
    }
}
