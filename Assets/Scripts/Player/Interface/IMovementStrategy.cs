using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy 
{
    void Move(Rigidbody2D rb, float dirX, float moveSpeed);
}
