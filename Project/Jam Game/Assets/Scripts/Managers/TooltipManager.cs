using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class TooltipManager : MonoBehaviourSingleton<TooltipManager>
{
    public GameObject tooltip;  

    public void SetUpTooltip(string desctiption, Vector2 position) {
        tooltip.SetActive(true);
        tooltip.GetComponentInChildren<TMP_Text>().text = desctiption;
        tooltip.transform.position = position;
    }

    public void CloseTooltip() {
        tooltip.SetActive(false);
    }

    
}
