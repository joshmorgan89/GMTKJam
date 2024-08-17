using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.Shared;
public class AsteroidMovement : BaseMovement
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    public override void Movement() {
        _rb.velocity = transform.right * MovementSpeed;
    }

    public override void Rotation() {
        gameObject.transform.Rotate(0,0,RotationSpeed);
    }
}
