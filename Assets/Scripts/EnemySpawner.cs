using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float tick;
    public int i;
    public GameObject enemy;
    public Panels door;
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

        if (door.open)
        {
            if (tick > 1 && i < 10)
            {
                GameObject tempAlien = Instantiate(enemy);
                aliens.Add(tempAlien.GetComponent<EnemiesHabilities>());
                i++;
                tick = 0;
            }
            tick += Time.deltaTime;
        }
        
    }
}
