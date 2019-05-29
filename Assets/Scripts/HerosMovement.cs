using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
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
    public float gravity;
    public float jumpForce;
    public bool enableCounterWind;
    public KeyCode jumpButton;
    public float counterWindForce;
    public KeyCode counterWindButton;
    public bool isOnAir;
    public bool canGround;
    public bool grounded;
    public bool onWind;
    private float direction;
    private Vector3 finalSpeed;
    private Vector3 lastFacing;
    

    // Start is called before the first frame update
    void Start()
    {
        lastFacing = Vector3.right;
        horizontalAxisName = horizontalAxisName + player;
        Debug.Log(horizontalAxisName);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb.gravityScale = gravity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = Input.GetAxis(horizontalAxisName);
                


        if (Input.GetKeyDown(jumpButton))
        {
            //anim.Play("Hit");
            //anim.Play("Jump");
            Jump();
        }

        if (enableCounterWind)
        {
            CounterWind();
        }

        SetFacing(direction);

        Move(direction);
        

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

    public Vector3 GetFacing()
    {
        Vector3 facing = Vector3.zero;
        if (direction < 0)
        {
            facing = Vector3.left;
            lastFacing = facing;
        }
        else if (direction > 0)
        {
            facing = Vector3.right;
            lastFacing = facing;
        }
        else if (direction == 0)
        {
            facing = lastFacing;
        }
        return facing;
    }

    private void Move(float horizontal)
    {
        finalSpeed = new Vector3(horizontal * speed, rb.velocity.y, 0);
        rb.velocity = finalSpeed;
    }

    private void Jump()
    {
        if (grounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }
    
    }

    private void CounterWind()
    {
        if (!grounded && onWind)
        {
            if (Input.GetKey(counterWindButton))
            {
                rb.AddForce(Vector3.down * counterWindForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            onWind = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            onWind = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);        
        

        if (collision.gameObject.layer == 13)
        {
            foreach(ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y > 0)
                {
                    grounded = true;
                }
                else grounded = false;
            }
        }

        if (collision.gameObject.layer == 10)
        {           
            transform.SetParent(collision.transform);
            grounded = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 13) || (collision.gameObject.layer == 10))
        {
            grounded = true;
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

