using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    public Transform spawn;
    public GameObject dust;
    public float damage;
    public BoxCollider2D otherCollider;

    private void DoDamage(float amount, GameObject go)
    {
        go.GetComponent<Damaggeable>().GetDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (hitPos.normal.y == 1)
            {
                Debug.Log(gameObject.name);
                Debug.Log(hitPos.normal.y);
                Instantiate(dust, spawn.position, Quaternion.identity);
                if (collision.gameObject.layer == 8)
                {
                    collision.gameObject.GetComponent<HerosMovement>().KnockUp();
                    DoDamage(damage, collision.gameObject);
                    collision.gameObject.GetComponent<HerosMovement>().KnockUp();
                }
            }
        }                
    }

    
}
