using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax_controller : MonoBehaviour
{
    public float layerSpeed;
    public float resetDistance;
    private float parentPos;

    void Update()
    {
        parentPos = this.transform.parent.position.x;
        if (transform.position.x < resetDistance + parentPos) 
            transform.position = new Vector3(parentPos,0,0);
        else
        transform.position -= new Vector3(layerSpeed,0,0) * Time.deltaTime;
    }
}
