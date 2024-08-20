using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public Transform HoldPosition;

    public BoxWithRooms CarriedBox {  get; set; }
    public Transform PodTransform => transform;

    private void Update() {
        if (CarriedBox != null) {
            // Make the box follow the drone
            CarriedBox.transform.position = HoldPosition.position;
        }
    }

    public void PlaceBox() {
        if (CarriedBox != null) {
            // Generate rooms at the box's position
            if (GameManager.Instance.GenerateRoomClusterAtPosition(CarriedBox.transform.position, CarriedBox.NumberOfRooms) == true) {
                // Destroy the box after placing it
                Destroy(CarriedBox.gameObject);

                // Clear the carried box reference
                CarriedBox = null;  
            }
        }
    }

    public bool IsPodOutsideCircle(Vector3 circleCenter, float radius) {
        float distance = Vector3.Distance(transform.position, circleCenter);
        return distance > radius;
    }

    public void MovePodOutsideShipBounds(Vector3 circleCenter, float radius) {
        float distance = Vector3.Distance(transform.position, circleCenter);

        if (distance <= radius) {
            Vector3 direction = (transform.position - circleCenter).normalized;
            
            // Move pod outside by 1 unit more than radius
            transform.position = circleCenter + direction * (radius + 1f); 
        }
    }
}
