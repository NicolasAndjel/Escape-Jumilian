using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenHeroMovement : HerosMovement
{
    public bool canDJ = false;
    public float oxygenCost;

    public override void Update()
    {
        base.Update();
        if (isOnAir && Input.GetKeyDown(jumpButton))
        {
            DoubleJump();
        }
    }

    private void DoubleJump()
    {
        if (canDJ)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);            
            grounded = false;
            canDJ = false;
            GetComponent<Damaggeable>().health -= oxygenCost;
        }
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            Debug.Log(hitPos.normal.y);
            if (hitPos.normal.y == 1)
            {
                isOnAir = false;
                grounded = true;
                canDJ = false;               

                if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
                {
                    transform.SetParent(collision.transform);
                }
            }
            if (collision.gameObject.layer == 13) canDJ = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            grounded = false;
            canDJ = true;
        }

        if (collision.gameObject.layer == 10)
        {
            canDJ = true;
            grounded = false;
            transform.SetParent(null);
        }

        if (collision.gameObject.layer == 22)
        {
            canDJ = true;
            transform.SetParent(null);
        }

    }
}
