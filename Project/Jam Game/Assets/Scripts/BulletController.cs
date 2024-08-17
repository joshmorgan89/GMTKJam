using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;

    private void Start()
    {
        Movement();
        Destroy(gameObject, 10);
    }

    private void Movement() { 
        rigidbody.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy") {
            Destroy(gameObject);
            //call enemy's reduce health method
        }
    }
}
