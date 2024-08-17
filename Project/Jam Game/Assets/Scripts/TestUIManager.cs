using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviourSingleton<TestUIManager>
{
    public TMPro.TextMeshPro fuelText;

    public void UpdateResourceUI(float electricity, float fuel) {
        fuelText.text = "Fuel: " + fuel;
    }
}
