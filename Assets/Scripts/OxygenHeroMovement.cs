using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenHeroMovement : HerosMovement
{
    public bool canDJ;
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
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 22)
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y > 0)
                {
                    grounded = true;
                    canDJ = true;
                }
                else grounded = false;
            }
            if (collision.gameObject.layer == 22)
            {
                transform.SetParent(collision.transform);
            }
        }

        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
        {
            transform.SetParent(collision.transform);
            grounded = true;
            canDJ = true;
        }
    }
}
