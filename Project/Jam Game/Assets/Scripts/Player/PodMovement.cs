using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float speed;

    /// <summary>
    /// Add force to player drone object based on the input
    /// </summary>
    /// <param name="input"></param>
    public void Movement(Vector2 input) {
        playerRB.AddForce(new Vector2(input.x, input.y) * speed, ForceMode2D.Force);
    }

    public void BreakStart()
    {
        playerRB.drag = 2.5f;
    }

    public void BreakEnd() {
        playerRB.drag = 0.5f;
    }
}
