using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
    private float _maxFuel = 100;
    private float _fuel;
    public float rareMineral = 250;

    public void UpdateFuel(float amount) {
        _fuel += amount;

        if (_fuel > _maxFuel) { 
            _fuel = _maxFuel;
        }

        HUD.Instance.UpdateFuel(_maxFuel, _fuel);
    }

    public void UpdateRareMineral(float amount) {
        rareMineral += amount;
        
    }

}
