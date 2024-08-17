using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private SpriteRenderer asteroidSprite;
    private Rigidbody2D rigidbody;
    private void Awake()
    {
        asteroidSprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    public void Movement() {
        rigidbody.velocity = transform.right * movementSpeed;
    }

    public void Rotation() {
        asteroidSprite.gameObject.transform.Rotate(0,0,rotationSpeed);
    }
}
