using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        if (direction.x > 0)
        {
            dir = 1;
        }
        else if (direction.x < 0)
        {
            dir = -1;
        }
        transform.position += Vector3.right * dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 14)
        {
            Destroy(gameObject);
        }
    }
}
