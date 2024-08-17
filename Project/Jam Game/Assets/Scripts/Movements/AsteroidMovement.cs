using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.Shared;
public class AsteroidMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private SpriteRenderer asteroidSprite;
    private Rigidbody2D rigidbody;
    private Health health;
    private void Awake()
    {
        asteroidSprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        Movement();
        Rotation();

        if (health.IsDestroyed) {
            Destroy(gameObject);
        }
    }

    public void Movement() {
        rigidbody.velocity = transform.right * movementSpeed;
    }

    public void Rotation() {
        asteroidSprite.gameObject.transform.Rotate(0,0,rotationSpeed);
    }
}
