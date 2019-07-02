using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenHeroMovement : HerosMovement
{
    public bool canDJ = false;
    public float oxygenCost;
    public float canDjtimer;
    public bool didDJ;

    public override void Update()
    {
        base.Update();
        CanDj();
        if (isOnAir && Input.GetKeyDown(jumpButton))
        {
            DoubleJump();
        }
    }

    private void DoubleJump()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            if (canDJ && !didDJ && (GetComponent<Damaggeable>().health > (oxygenCost + 5)))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                grounded = false;
                canDJ = false;
                didDJ = true;
                GetComponent<Damaggeable>().health -= oxygenCost;
            }
        }
        
   }

  /*  private void OnCollisionEnter2D(Collision2D collision)
    {       
        foreach (ContactPoint2D hitPos in collision.contacts)
        {            
            if (hitPos.normal.y == 1)
            {
                isOnAir = false;
                grounded = true;
                canDJ = false;
                didDJ = false;

                if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
                {
                    transform.SetParent(collision.transform);
                }
            }
            if (collision.gameObject.layer == 13) canDJ = false;
        }
    }
    */

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (hitPos.normal.y == 1)
            {
                isOnAir = false;
                grounded = true;
                canDJ = false;
                didDJ = false;

                if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
                {
                    transform.SetParent(collision.transform);
                }
            }
            if (collision.gameObject.layer == 13) canDJ = false;
        }
    }

    public void CanDj()
    {
        if (isOnAir)
        {
            canDjtimer += Time.deltaTime;
        }
        if (canDjtimer > 0.15)
        {
            canDJ = true;
            canDjtimer = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentCollisions.Contains(collision.gameObject))
        {
            currentCollisions.Remove(collision.gameObject);
        }

        if (collision.gameObject.layer == 13)
        {
            canDJ = true;
            isOnAir = true;
            grounded = false;
        }

        if (collision.gameObject.layer == 10)
        {
            isOnAir = true;
            canDJ = true;
            grounded = false;
            transform.SetParent(null);
        }
        
        if (collision.gameObject.layer == 19)
        {
            isOnAir = true;
            canDJ = true;
            transform.SetParent(null);
        }

    }
}
