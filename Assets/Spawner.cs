using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float timer = 0;
    public float timeToSpawn;
    public GameObject prefab;
    public float counter;
    public float maxSpawn;
    
    void Update()
    {
        if ((counter < maxSpawn))
        {
            Spawn(true);
        }
    }

    public void Spawn(bool on)
    {
        if (on)
        {
            if (timer > timeToSpawn)
            {
                GameObject tempMosquito = Instantiate(prefab, transform.position, Quaternion.identity);
                tempMosquito.transform.GetChild(0).GetComponent<MosquitoBrain>().sp = this;
                counter++;
                timer = 0;
            }
            timer += Time.deltaTime;
        }
        else
        {
            if (timer > timeToSpawn)
            {
                GameObject tempMosquito = Instantiate(prefab, transform.position, Quaternion.identity);
                tempMosquito.transform.GetChild(0).GetComponent<EnemiesMovement>();
                counter++;
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
}
