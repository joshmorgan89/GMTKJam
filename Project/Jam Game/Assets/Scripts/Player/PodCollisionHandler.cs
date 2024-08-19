using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            gameObject.GetComponent<PlayerHealth>().PlayerCurrentHealthUpdate(-5);
            //enemy collision event
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BoxWithRooms box = collision.gameObject.GetComponent<BoxWithRooms>();
        InteractionHandler pod = GetComponent<InteractionHandler>();
        if (box != null && pod.CarriedBox == null) {
            pod.CarriedBox = box;
        }
    }
}
