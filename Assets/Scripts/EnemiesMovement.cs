using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : Platform
{
    public float saveSpeed;
    Vector3 facing;
    int lastDirection;
    SpriteRenderer sr;

    public override void Start()
    {
        base.Start();
        saveSpeed = speed;
        lastDirection = -direction;
        sr = GetComponent<SpriteRenderer>();
    }
    public override void Update()
    {
        Move();
        Facing();
    }

    public void Facing()
    {
        if (lastDirection == direction)
        {
            return;
        }
        else if (direction != lastDirection)
        {
            sr.flipX = !sr.flipX;
        }
        lastDirection = direction;
    }
    
}
