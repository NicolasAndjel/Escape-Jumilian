using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject hitting;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;
    public Vector3 direction;
    public float speed;
    public float time;
    public int dir;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position += direction * dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitting, GetHitSpawn().position, Quaternion.identity);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

    public Transform GetHitSpawn()
    {
        Transform a = null;
        if (dir == -1)
        {
            a = bulletSpawnLeft;
        }
        else
        {
            a = bulletSpawnRight;
        }
        return a;
    }
}
