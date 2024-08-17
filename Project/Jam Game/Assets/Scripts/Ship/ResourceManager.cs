using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
    private float _maxFuel = 100;
    private float _fuel;
    private float _electricity;

    public void UpdateMaxFuel(float amount) { 
        _maxFuel += amount;
    }

    public void UpdateFuel(float amount) {
        _fuel += amount;

        HUD.Instance.UpdateFuel(_maxFuel, _fuel);
    }

    public void UpdateElectricity(float amount) {  
        _electricity += amount;
    }

}
