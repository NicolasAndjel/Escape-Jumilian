using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class HerosMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    public string player;
    public string horizontalAxisName;
    public float speed;
    public float jumpForce;
    public KeyCode jumpButton;
    public bool isOnAir;
    public bool canGround;
    public bool grounded;





    // Start is called before the first frame update
    void Start()
    {
        horizontalAxisName = "LeftstickhorizontalP1";
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(horizontalAxisName);

        if (Input.GetKeyDown(jumpButton))
        {
            //anim.Play("Hit");
            //anim.Play("Jump");
            Jump();
        }

        SetFacing(horizontal);

        Move(horizontal);
        

        //anim.SetFloat("WalkSpeed", Mathf.Abs(horizontal));

        if (isOnAir)
        {
            if (rb.velocity.y > 0)
            {
                //anim.Play("Jump");
            }
            else if (rb.velocity.y < 0)
            {
                //anim.Play("Fall");
            }
        }

    }

    private void SetFacing(float horizontal)
    {
        if (horizontal < 0)
        {
            sr.flipX = true;
        }
        else if (horizontal > 0)
        {
            sr.flipX = false;
        }
    }

    public Vector3 GetFacing(Vector3 direction)
    {
        Vector3 facing = Vector3.zero;
        if (direction.x < 0)
        {
            facing = Vector3.left;
        }
        else if (direction.x > 0)
        {
            facing = Vector3.right;
        }
        return facing;
    }

    private void Move(float horizontal)
    {
        Vector3 finalSpeed = new Vector3(horizontal * speed, rb.velocity.y, 0);
        rb.velocity = finalSpeed;
    }

    void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 13)
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {

                Debug.Log(hitPos.normal);
                if (((hitPos.normal.x > 0) || (hitPos.normal.x < 0) || (hitPos.normal.y < 0)) && (!grounded))
                {
                    canGround = false;
                }
                else canGround = true;
            }
            if (canGround)
            {
                grounded = true;
            }            
        }

        if (collision.gameObject.layer == 10)
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                Debug.Log(hitPos.normal);
                if (((hitPos.normal.x > 0) || (hitPos.normal.x < 0) || (hitPos.normal.y < 0)) && (!grounded))
                {
                    canGround = false;
                }
                else canGround = true;
            }
            if (canGround)
            {
                transform.SetParent(collision.transform);
                grounded = true;
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            grounded = false;            
        }

        if (collision.gameObject.layer == 10)
        {
            grounded = false;            
            transform.SetParent(null);
        }

    }
}

