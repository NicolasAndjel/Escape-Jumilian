﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class HerosMovement : MonoBehaviour
{    
    [HideInInspector]
    public Rigidbody2D rb;
    SpriteRenderer sr;    
    public Animator anim;
    public AudioSource aS;
    public string player;
    public string horizontalAxisName;
    public float speed;
    public float gravity;
    public float jumpForce;
    public bool enableCounterWind;
    public KeyCode jumpButton;
    public KeyCode jumpButtonJoy;
    public float counterWindForce;
    public KeyCode counterWindButton;
    public float knockUpAmount; 
    public bool isOnAir;
    public bool canGround;
    public bool grounded;
    public bool onWind;    
    [HideInInspector]
    public float direction;
    private Vector3 finalSpeed;
    private Vector3 lastFacing;
    public List<GameObject> currentCollisions;
    public bool death;
    public AudioClip jumpSound;
    public PlayersHabilities ph;
    

    // Start is called before the first frame update
    public virtual void Start()
    {        
        currentCollisions = new List<GameObject>();
        lastFacing = Vector3.right;
        horizontalAxisName = horizontalAxisName + player;       
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();
        ph = GetComponent<PlayersHabilities>();
        rb.gravityScale = gravity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!death)
        {       

            direction = Input.GetAxis(horizontalAxisName);

            if (Input.GetKeyDown(jumpButton) || Input.GetButtonDown(ph.fireButton))
            {
                if (grounded)
                    Jump();
            }

            if (enableCounterWind)
            {
                CounterWind();
            }

            SetFacing(direction);

            Move(direction);
        }
    }

    private void LateUpdate()
    {
        MoveSound();
    }

    private void MoveSound()
    {
        if (direction != 0 && grounded)
        {
            if (!aS.isPlaying)
            {
                aS.Play();
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
        anim.SetFloat("WalkSpeed", Mathf.Abs(finalSpeed.normalized.x));
        anim.SetFloat("JumpSpeed", rb.velocity.normalized.y);
        anim.SetBool("IsJumping", isOnAir);        
    }

    public void Jump()
    {        
        isOnAir = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
        aS.PlayOneShot(jumpSound);
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

    public void KnockUp()
    {
        rb.velocity = Vector3.zero;
        Vector3 force = Vector3.up * knockUpAmount;
        rb.AddForce(force, ForceMode2D.Impulse);       
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
        if (collision.gameObject.layer == 10)
        {
            if (!isOnAir)
            {
                grounded = true;
                isOnAir = false;
            }
        }
       
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (hitPos.normal.y == 1)
            {                
                isOnAir = false;
                grounded = true;               

                if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
                {
                    transform.SetParent(collision.transform);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            grounded = false;
        }

        if (collision.gameObject.layer == 13)
        {
     
            grounded = false;           
        }

        if (collision.gameObject.layer == 10)
        {
            if (!isOnAir)
            {
                grounded = false;
                transform.SetParent(null);
            }             
        }

        if (collision.gameObject.layer == 19)
        {
            grounded = false;
            transform.SetParent(null);           
        }       
    }
}

