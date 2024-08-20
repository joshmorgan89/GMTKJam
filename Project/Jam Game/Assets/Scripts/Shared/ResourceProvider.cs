using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceDrop { 
    rareMineral,
    Refugees
}

public class ResourceProvider : MonoBehaviour
{
    public ResourceDrop resourceDrop;
    public float resourceMultiplier;
    private void OnDestroy()
    {
        //provide material
        if (resourceDrop == ResourceDrop.rareMineral) {
            ResourceManager.Instance.UpdateRareMineral(5 * resourceMultiplier);
        }

        if (resourceDrop == ResourceDrop.Refugees) {
            ResourceManager.Instance.UpdateRefugee(Settings.Instance.RefugeeDropModifier);
        }
    }
}
