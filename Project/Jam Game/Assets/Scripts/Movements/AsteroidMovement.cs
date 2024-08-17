using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.Shared;
public class AsteroidMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private SpriteRenderer _asteroidSprite;
    private Rigidbody2D _rigidbody;
    private Health _health;
    private void Awake()
    {
        _asteroidSprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        Movement();
        Rotation();

        if (_health.IsDestroyed) {
            Destroy(gameObject);
        }
    }

    public void Movement() {
        _rigidbody.velocity = transform.right * movementSpeed;
    }

    public void Rotation() {
        _asteroidSprite.gameObject.transform.Rotate(0,0,rotationSpeed);
    }
}
