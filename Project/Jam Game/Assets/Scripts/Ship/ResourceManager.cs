using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
    public float fuel;
    public float electricity;

    public void UpdateFuel(float amount) {
        fuel += amount;
        Tester.Instance.UpdateResourceUI(electricity, fuel);
    }

    public void UpdateElectricity(float amount) {  
        electricity += amount;
    }

}
