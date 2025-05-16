using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : IJumpStrategy
{
    public void Jump(Rigidbody2D rb, ref int jumpCount, int maxJumpCount, float jumpForce)
    {
        if (jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
