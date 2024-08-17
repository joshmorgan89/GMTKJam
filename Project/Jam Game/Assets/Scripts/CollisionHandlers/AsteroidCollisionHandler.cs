using Scripts.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//require a ResourceProvider and Health component
[RequireComponent(typeof(ResourceProvider),typeof(Health))]
public class AsteroidCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Bullet") {
            GetComponent<Health>().CurrentHealthUpdate(-50);
        }

        
    }

}
