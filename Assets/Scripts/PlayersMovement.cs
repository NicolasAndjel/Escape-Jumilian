using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
	public enum states
	{
		MOVE,
		STOP,
		TOOFAR,
	}

    public string player;
    public Vector3 direction;    
    public Vector3 facing;
    public Vector3 prevFacing;
    public string horizontal;
    public string vertical;
    public KeyCode jumButton;
    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    private float jumpTimeCounter;
    public float gravity;
    private float saveGravity;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public bool canGround;
    public bool grounded;    
    public bool isJumping;
    public bool canDJ;
    public BoxCollider2D floor;
    public BoxCollider2D playerCollider; 
    public states state;
    float checkColl;
    

    // Start is called before the first frame update

    private void Awake()
    {
        canDJ = true;
        checkColl = 0;
        canGround = true;
    	state = states.MOVE;
        isJumping = false;
        saveGravity = gravity;
        facing = Vector3.right;
        prevFacing = facing;
        playerCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }   

    private void Update()
    {
    	if (state == states.MOVE)
    	{
    		Move();
        	GetInput();
    	}
    	else if (state == states.TOOFAR)
    	{
    		GetAxis();
        	GetFacing();
        	Facing();
            if (facing == Vector3.right)
            {
                if (direction.x > 0)
                {
                    direction.x = 0;
                }
            }
            else if (facing == Vector3.right)
            {
                if (direction.x < 0)
                {
                    direction.x = 0;
                }
            }         
            
			Move();
        	GetInput();
    	}
        
    }

    private void GetInput()
    {
        JumpGroup(jumButton);
    }

    private void GetAxis()
    {
        direction.x = Input.GetAxisRaw(horizontal + player) * speed;
    }

    public Vector3 GetFacing()
    {
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

    private void Move()
    {        
    	GetAxis();
        GetFacing();
        Facing();
        rb.velocity = direction;
        if (direction.y < -20)
        {
            grounded = true;
        }        
        Gravity();
    }

    public void Gravity()
    {
        if (!grounded)
        {
            direction.y = direction.y - (gravity * Time.deltaTime);
        }
    }

    private void Facing()
    {
        if (facing != prevFacing)
        {
            sr.flipX = !sr.flipX;
        }
        prevFacing = facing;
    }

    private void Jump(bool on)
    {        
        if (on && grounded)
        {            
            grounded = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            direction.y = jumpSpeed;
        }        

    }

    private void HoldJump(bool on)
    {
        if (on && isJumping)
        {
            if (jumpTimeCounter > 0f)
            {
                direction.y = jumpSpeed;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
    }

    public void DoubleJump(bool on)
    {
        if (on && canDJ)
        {
            
            canDJ = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            direction.y += jumpSpeed;
        }
    }

    private void JumpGroup(KeyCode kc)
    {
        Jump(Input.GetKeyDown(kc));
        HoldJump(Input.GetKey(kc));
        StopJump(Input.GetKeyUp(kc));
    }

    private void StopJump(bool on)
    {
        if (on)
        {
            isJumping = false;
        }
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
            gravity = saveGravity;  
            if (canGround)
            {
                grounded = true;
            }
            direction.y = 0;
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
            if (!isJumping)
            {
                transform.parent = collision.transform;
            }
            if (canGround)
            {
                grounded = true;
            }
            gravity = saveGravity;
        }

        if (collision.gameObject.layer == 14)
        {
            bool fallFast = true;
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                Debug.Log(hitPos.normal);
                if ((hitPos.normal.y == 1))
                {
                    fallFast = false;
                }
                else fallFast = true;
            }
            if (fallFast)
            {
                gravity *= 2;
            }
            else grounded = true;
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
            grounded = false;
            canDJ = true;
            transform.parent = null;
        }

        checkColl = 0;
    }
    
}
           

       