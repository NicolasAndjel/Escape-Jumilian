using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    public Transform spawn;
    public GameObject dust;
    public float damage;

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
                Debug.Log(hitPos.normal.y);
                if (collision.gameObject.layer == 8)
                {
                    DoDamage(damage, collision.gameObject);
                    collision.gameObject.GetComponent<HerosMovement>().KnockUp();
                }                
                Instantiate(dust, spawn.position, Quaternion.identity);
            }
        }                
    }

    
}
