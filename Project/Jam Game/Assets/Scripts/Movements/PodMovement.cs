using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float speed;


    private void Update()
    {
        Vector2 direction = playerRB.velocity;

        if (direction.magnitude > 0.1f)
        {
            SnapRotationToNearest90Degrees(direction);
        }
    }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRB.rotation = angle - 90f;
    }
    private void SnapRotationToNearest90Degrees(Vector2 direction)
    {
        if (Input.GetKeyDown("w")) {
            playerRB.rotation = 0f;
        }
        else if (Input.GetKeyDown("a"))
        {
            playerRB.rotation = 90f;
        }
        else if(Input.GetKeyDown("s"))
        {
            playerRB.rotation = 180f;
        }
        else if(Input.GetKeyDown("d"))
        {
            playerRB.rotation = 270f;
        }


        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //
        //float snappedAngle = Mathf.Round(angle / 90f) * 90f;
        //
        //playerRB.rotation = snappedAngle-90;
    }

    /// <summary>
    /// Add force to player drone object based on the input
    /// </summary>
    /// <param name="input"></param>
    public void Movement(Vector2 input) {
        playerRB.AddForce(new Vector2(input.x, input.y) * speed * Settings.Instance.PlayerSpeedMultiplier, ForceMode2D.Force);
    }

    public void BreakStart()
    {
        playerRB.drag = 2.5f;
    }

    public void BreakEnd() {
        playerRB.drag = 0.5f;
    }
}
