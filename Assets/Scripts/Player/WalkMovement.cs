using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : IMovementStrategy
{
    public void Move(Rigidbody2D rb, float dirX, float moveSpeed)
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
}
