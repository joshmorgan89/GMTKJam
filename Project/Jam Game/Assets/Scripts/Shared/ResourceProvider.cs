using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceDrop { 
    fuel
}

public class ResourceProvider : MonoBehaviour
{
    public ResourceDrop resourceDrop;
    public float resourceMultiplier;
    private void OnDestroy()
    {
        //provide material
        if (resourceDrop == ResourceDrop.fuel) {
            ResourceManager.Instance.UpdateRareMineral(5 * resourceMultiplier);
        }
    }
}
