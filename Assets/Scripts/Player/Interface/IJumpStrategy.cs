using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpStrategy 
{
    void Jump(Rigidbody2D rb, ref int jumpCount, int maxJumpCount, float jumpForce);
}
