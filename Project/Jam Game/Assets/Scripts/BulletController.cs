using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement() { 
        rigidbody.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
            //call enemy's reduce health method
        }
    }
}
